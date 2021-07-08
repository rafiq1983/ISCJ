using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.Registration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using FluentValidation.Results;
using MA.Common.Entities;
using MA.Common.Entities.MasjidMembership;
using MA.Common.Entities.Product;
using MA.Common.Entities.School;
using MA.Common.Entities.Student;
using MA.Common.Models.api;
using MA.Core;

namespace BusinessLogic
{

  public class RegistrationManager
  {
      private void AddMembershipIfNeeded(CallContext context, CreateRegistrationApplicationInput input)
      {
          MasjidMembershipManager mgr = new MasjidMembershipManager();
          MasjidMembership membership;

          if (input.AddFatherMembership)
          {
               membership = mgr.GetMembershipByContactId(input.FatherId.GetValueOrDefault());

              if (membership == null)
              {
                  mgr.CreateMasjidMembership(context, new CreateMasjidMembershipInput()
                  {
                      ContactId = input.FatherId,
                      AddNewContact = false,
                      BillingInstructions = new List<ProductSelected>(),
                      EffectiveDate = DateTime.UtcNow,
                      ExpirationDate = DateTime.UtcNow.AddYears(1)

                  });
              }
          }

          if (input.AddMotherToMembership)
          {
              membership = mgr.GetMembershipByContactId(input.MotherId.GetValueOrDefault());

              if (membership == null)
              {
                  mgr.CreateMasjidMembership(context, new CreateMasjidMembershipInput()
                  {
                      ContactId = input.MotherId,
                      AddNewContact = false,
                      BillingInstructions = new List<ProductSelected>(),
                      EffectiveDate = DateTime.UtcNow,
                      ExpirationDate = DateTime.UtcNow.AddYears(1)

                  });
              }
          }

      }
    public CreateRegistrationApplicationOutput CreateRegistration(CallContext context, CreateRegistrationApplicationInput input)
    {
            using (TransactionScope scope = new TransactionScope())
            {
                var registrationApplication = SaveRegistrationApplication(context, input);
                AddMembershipIfNeeded(context, input);

                var invoiceTypeId = AddRegistrationApplicationInvoiceTypeId(context);

                PerformBilling(context, input.BillingInstructions, registrationApplication.ApplicationId,
                    ReferenceType.RegistrationApplication, invoiceTypeId, input.FatherId.Value);

              AddStudents(context, input, registrationApplication, input.AutoAssignSubjects);
                
                scope.Complete();
                return new CreateRegistrationApplicationOutput() { ApplicationId = registrationApplication.ApplicationId};
            }
    }

    private Guid AddRegistrationApplicationInvoiceTypeId(CallContext context)
    {
        using (var db = new Database())
        {
            var invoiceTypeID = db.InvoiceTypes.SingleOrDefault(x => x.InvoiceTypeName == "Registration-Charges")
                ?.InvoiceTypeId;

            if (invoiceTypeID == null)
            {
                invoiceTypeID = Guid.NewGuid();
                db.InvoiceTypes.Add(new InvoiceType()
                {
                    InvoiceTypeId = invoiceTypeID.Value,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = context.UserLoginName,
                    InvoiceTypeName = "Registration-Charges",
                    ShortDescription = "Registration Application Fees",
                    TenantId = context.TenantId.Value

                });

                db.SaveChanges();

            }

            return invoiceTypeID.Value;
        }
    }

    private void AddStudents(CallContext context, CreateRegistrationApplicationInput input, RegistrationApplication app, bool assignSubjects)
    {
        StudentManager mgr = new StudentManager();
        ProgramManager programManager = new ProgramManager();
        foreach (var reg in input.StudentRegistrations)
        {
            if (reg.StudentId.HasValue == false)
                continue;
            var student = mgr.GetStudentByContactId(context, reg.StudentId.Value);

            if (student == null)
            {
                SequenceCounter counter;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))//creating in its own transaction as don't want to block that table for too long.
                {

                    using (var db2 = new Database())
                    {
                        counter = db2.SequenceCounters.Single(x =>
                            x.TenantId == context.TenantId && x.CounterName == "StudentCounter");

                        db2.SequenceCounters.Add(counter);
                        counter.CounterValue += 1;
                        db2.Entry(counter).State = EntityState.Modified;
                        db2.SaveChanges(); //update counter right away so we other transactions are not blocked from updating the value.


                    }
                    scope.Complete();
                }

                    mgr.AddStudent(context, new Student()
                {
                    StudentId = reg.StudentId.Value,
                    ContactId = reg.StudentId.Value,
                    FatherContactId = input.FatherId.Value,
                    MotherContactId = input.MotherId.Value,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = context.UserLoginName,
                    TenantId = context.TenantId.Value,
                    StudentNumber = counter.CounterValue

                });
            }

            if (assignSubjects)
            {
                //Add subjects.
                CourseManager courseManager = new CourseManager();

                List<SubjectMapping> subjectMappings =
                    courseManager.GetSubjectByProgramAndIslamicGradeId(context, input.ProgramId.Value,
                        reg.IslamicSchoolGrade);

                foreach (var subject in subjectMappings)
                {
                    var enrollmentId = app.Enrollments.SingleOrDefault(x => x.StudentContactId == reg.StudentId)
                        .EnrollmentId;

                    var studentSubjectId = mgr.AddStudentSubject(context, reg.StudentId.Value, enrollmentId,
                        subject.SubjectId, input.ProgramId.Value);

                    mgr.AddMetricsToStudentSubject(context, studentSubjectId,
                        programManager.GetAssociatedMetrics(context, subject.SubjectId).Select(x => x.MetricId)
                            .ToList());
                }
            }
        }

    }


    public List<RegistrationApplication> GetAllApplications(CallContext context, Guid programId)
    {
        using (var db = new Database())
        {
            var query = db.RegistrationApplications.AsQueryable();

            if (programId != Guid.Empty)
                query = db.RegistrationApplications.Where(x => x.ProgramId == programId);

            query = query.Include(x => x.MotherContactInfo)
                    .Include(x => x.FatherContactInfo);

            var applications = query.ToList();
            return applications;
        }
    }

    public RegistrationApplication GetApplication(CallContext context, Guid regisId)
    {
        using (var db = new Database())
        {
            return db.RegistrationApplications.SingleOrDefault(x => x.ApplicationId == regisId);
           
        }
    }


        public AddRegistrationOutput AddEnrollmentToRegistrationApplication(CallContext context, AddRegistrationInput input)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            using (var db = new Database())
            {
               
                var enrollment = new Enrollment();
                var existingApplication =
                    db.RegistrationApplications.Where(x => x.ApplicationId == input.RegistrationApplicationId).Include(x=>x.Enrollments).Single();

                enrollment.ProgramId = existingApplication.ProgramId;
                enrollment.StudentContactId = input.StudentId.GetValueOrDefault(Guid.Empty);
                enrollment.IslamicSchoolGradeId = input.IslamicSchoolGrade;
                enrollment.PublicSchoolGradeId = input.PublicSchoolGrade;
                enrollment.CreateUser = context.UserLoginName;
                enrollment.CreateDate = DateTime.UtcNow;
                //TODO: FOR Rafiq: Why Father and Mother id not getting populated automatically from parent.  Are relationships missing?
                enrollment.FatherId = existingApplication.FatherContactId;
                enrollment.MotherId = existingApplication.MotherContactId;
                
                existingApplication.Enrollments.Add(enrollment);
              PerformBilling(context, input.BillingInstructions, existingApplication.ApplicationId, ReferenceType.RegistrationApplication, Guid.Empty, existingApplication.FatherContactId);
                db.SaveChanges();
            }

            scope.Complete();
            return new AddRegistrationOutput()
            {
                Success = true
            };

        }
    }

        public AddEnrollmentOutput AddEnrollment(CallContext context, AddEnrollmentInput input)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var db = new Database())
                {

                    var enrollment = GetStudentEnrollment(input.StudentId,input.ProgramId);

                    if (enrollment != null)
                        return new AddEnrollmentOutput()
                        {
                            Success = false,
                            Message = "Enrollment already exists for student."
                        };

                    enrollment = new Enrollment();

                    enrollment.ProgramId = input.ProgramId;
                    enrollment.StudentContactId = input.StudentId;
                    enrollment.IslamicSchoolGradeId = input.IslamicSchoolGrade;
                    enrollment.PublicSchoolGradeId = input.PublicSchoolGrade;
                    enrollment.CreateUser = context.UserLoginName;
                    enrollment.CreateDate = DateTime.UtcNow;
                    enrollment.FatherId = input.FatherId;
                    enrollment.MotherId = input.MotherId;
                    enrollment.RegistrationApplicationId = Guid.Empty;
                    db.Enrollments.Add(enrollment);
                    var invoiceTypeId = Guid.Empty;//GetEnrollmentFeeCharges();
                    PerformBilling(context, input.BillingInstructions, enrollment.EnrollmentId, ReferenceType.Enrollment, invoiceTypeId, enrollment.FatherId);
                    db.SaveChanges();
                }

                scope.Complete();
                return new AddEnrollmentOutput()
                {
                    Success = true
                };

            }
        }

        private RegistrationApplication SaveRegistrationApplication(CallContext context, CreateRegistrationApplicationInput input)
                {
            using (var db = new Database())
            {
                SequenceCounter counter;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))//creating in its own transaction as don't want to block that table for too long.
                {

                    using (var db2 = new Database())
                    {
                        counter = db2.SequenceCounters.Single(x =>
                            x.TenantId == context.TenantId && x.CounterName == "RegistrationApplicationCounter");

                        db2.SequenceCounters.Add(counter);
                        counter.CounterValue += 1;
                        db2.Entry(counter).State = EntityState.Modified;
                        db2.SaveChanges(); //update counter right away so we other transactions are not blocked from updating the value.


                    }
                    scope.Complete();
                }

                RegistrationApplication application = new RegistrationApplication()
                {
                    ApplicationId = Guid.NewGuid(),
                    ApplicationDate = DateTime.UtcNow,
                    FatherContactId = input.FatherId.GetValueOrDefault(Guid.Empty),
                    MotherContactId = input.MotherId.GetValueOrDefault(Guid.Empty),
                    ProgramId = input.ProgramId.GetValueOrDefault(Guid.Empty),
                    MembershipId = Guid.Empty,
                    TenantId = context.TenantId.Value,
                    ApplicationNumber = counter.CounterValue,
                    CreateUser = context.UserLoginName
                };
               
                application.Enrollments = new List<Enrollment>();

                foreach (var reg in input.StudentRegistrations)
                {
                    if (reg.StudentId.HasValue == false)
                        continue;

                    application.Enrollments.Add(new Enrollment()
                    {
                        FatherId = input.FatherId.Value,
                        MotherId = input.MotherId.Value,
                        ProgramId = input.ProgramId.Value,
                        IslamicSchoolGradeId = reg.IslamicSchoolGrade,
                        PublicSchoolGradeId = reg.PublicSchoolGrade,
                        EnrollmentId = Guid.NewGuid(),
                        StudentContactId = reg.StudentId.Value,
                        CreateDate = DateTime.UtcNow,
                        CreateUser = context.UserLoginName,
                        TenantId = context.TenantId.Value
                    });
                }
                
                db.RegistrationApplications.Add(application);
                                
                db.SaveChanges();
                return application;
            }

        }

    
    private void PerformBilling(CallContext context, List<ProductSelected> billingInstructions, Guid registrationApplicationId, ReferenceType referenceType, Guid invoiceTypeId, Guid responsiblePartyId)
        {
            if (billingInstructions==null && billingInstructions.Count == 0)
                return;

            using (var db = new Database())
            {
                var productIds = billingInstructions.Where(y => y.IsSelected).Select(z => z.ProductCode).ToArray();
                ProductManager mgr = new ProductManager();
                var billableProducts = mgr.GetAllProducts(context);

                var products = billableProducts.Where(x => productIds.Contains(x.ProductCode)).ToDictionary(y=>y.ProductCode);

                Invoice invoice = new Invoice();
                invoice.DueDate = DateTime.UtcNow;
                invoice.GenerationDate = DateTime.UtcNow;
                invoice.CreateUser = context.UserLoginName;
                invoice.ReferenceId = registrationApplicationId.ToString();
                invoice.ReferenceType = referenceType;
                invoice.TennantId = context.TenantId.Value;
                invoice.CreateDate = DateTime.UtcNow;
                invoice.ModifiedDate = null;
                invoice.InvoiceTypeId = invoiceTypeId;
                invoice.ResponsiblePartyId = responsiblePartyId;

                invoice.InvoiceItems = new List<InvoiceItem>();
                foreach (var item in billingInstructions.Where(x=>x.IsSelected))
                {
                    if(products.ContainsKey(item.ProductCode))
                       {
                        invoice.InvoiceAmount += item.ProductCount * products[item.ProductCode].Price;
                        invoice.InvoiceItems.Add(new InvoiceItem()
                        {
                            Amount = products[item.ProductCode].Price * item.ProductCount,
                            Description = products[item.ProductCode].Description,
                            Quantity = products[item.ProductCode].SelectedCount,
                            CreateUser = context.UserLoginName,
                            CreateDate = DateTime.UtcNow,
                            TenantId = context.TenantId.Value
                        });
                    }                    
                    else
                    {
                        throw new Exception("Invalid Product id " + item.ProductId);
                    }
                }

                    invoice.Description = "Invoice generated during registration application.";

                    db.Invoices.Add(invoice);
                db.SaveChanges();
                }
            }
    
        public Guid CreateEnrollment(Guid programId, 
                      Guid studentId, Guid fatherId, Guid motherId,
                      string islamicSchoolGrade, string publicSchoolGrade)
    {
      return Guid.Empty;
    }

    public Enrollment GetEnrollment(Guid enrollmentId)
    {
      using (var db = new Database())
      {
        return db.Enrollments.SingleOrDefault(x => x.EnrollmentId == enrollmentId);
      }
    }

    public Enrollment GetStudentEnrollment(Guid studentId, Guid programId)
    {
        using (var db = new Database())
        {
            return db.Enrollments.SingleOrDefault(x => x.StudentContactId == studentId && x.ProgramId == programId);
        }
    }

        public List<Enrollment> GetEnrollments()
    {
      using (var db = new Database())
      {

        return db.Enrollments
          .Include(Registration => Registration.FatherContactInfo)
          .Include(registration=>registration.MotherContactInfo)
          .Include(Registration=>Registration.StudentContactInfo)
          .ToList();
      }
    }

    

    public List<Enrollment> GetEnrollments(Guid programId, Guid? registrationApplicationId = null)
    {
      using (var db = new Database())
      {
          var query = db.Enrollments
              .Include(Registration => Registration.FatherContactInfo)
              .Include(registration => registration.MotherContactInfo)
              .Include(Registration => Registration.StudentContactInfo)
              .Include(Registration => Registration.RegistrationApplication)
              .Where(x => x.ProgramId == programId);

        if (registrationApplicationId != null)
        {
           query = query.Where(x => x.RegistrationApplicationId == registrationApplicationId.Value);
        }

        return query.ToList();
      }
    }

    //important name it different than GetEnrollments above.
   public List<Enrollment> GetEnrollmentsByParentId(Guid parentID)
        {
            return this.GetEnrollments().Where(x => x.FatherId == parentID || x.MotherId == parentID).ToList<Enrollment>();
       
        }

  
  }
}
