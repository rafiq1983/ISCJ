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
using MA.Common.Entities.Product;
using MA.Common.Models.api;
using MA.Core;

namespace BusinessLogic
{

  public class RegistrationManager
  {
      private void AddMembershipIfNeeded(CallContext context, CreateRegistrationApplicationInput input)
      {
          MasjidMembershipManager mgr = new MasjidMembershipManager();
         var membership = mgr.GetMembershipByContactId(input.FatherId.GetValueOrDefault());

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
    public CreateRegistrationApplicationOutput CreateRegistration(CallContext context, CreateRegistrationApplicationInput input)
    {
            using (TransactionScope scope = new TransactionScope())
            {
                var registrationApplicationId = SaveRegistrationApplication(context, input);
                AddMembershipIfNeeded(context, input);
                PerformBilling(context, input.BillingInstructions, registrationApplicationId,
                    InvoiceOrderType.RegistrationApplication);
                scope.Complete();
                return new CreateRegistrationApplicationOutput() { ApplicationId = registrationApplicationId};
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
                enrollment.CreateUser = context.UserId;
                enrollment.CreateDate = DateTime.UtcNow;
                //TODO: FOR Rafiq: Why Father and Mother id not getting populated automatically from parent.  Are relationships missing?
                enrollment.FatherId = existingApplication.FatherContactId;
                enrollment.MotherId = existingApplication.MotherContactId;
                
                existingApplication.Enrollments.Add(enrollment);
              PerformBilling(context, input.BillingInstructions, existingApplication.ApplicationId, InvoiceOrderType.RegistrationApplication );
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
                    enrollment.CreateUser = context.UserId;
                    enrollment.CreateDate = DateTime.UtcNow;
                    enrollment.FatherId = input.FatherId;
                    enrollment.MotherId = input.MotherId;
                    enrollment.RegistrationApplicationId = Guid.Empty;
                    db.Enrollments.Add(enrollment);
                    PerformBilling(context, input.BillingInstructions, enrollment.EnrollmentId, InvoiceOrderType.Enrollment);
                    db.SaveChanges();
                }

                scope.Complete();
                return new AddEnrollmentOutput()
                {
                    Success = true
                };

            }
        }

        private Guid SaveRegistrationApplication(CallContext context, CreateRegistrationApplicationInput input)
                {
            using (var db = new Database())
            {   
                RegistrationApplication application = new RegistrationApplication()
                {
                    ApplicationId = Guid.NewGuid(),
                    ApplicationDate = DateTime.UtcNow,
                    FatherContactId = input.FatherId.GetValueOrDefault(Guid.Empty),
                    MotherContactId = input.MotherId.GetValueOrDefault(Guid.Empty),
                    ProgramId = input.ProgramId.GetValueOrDefault(Guid.Empty),
                    MembershipId = Guid.Empty,
                    CreateUser = context.UserId
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
                        CreateUser = context.UserId
                    });
                }
                
                db.RegistrationApplications.Add(application);
                                
                db.SaveChanges();
                return application.ApplicationId;
            }

        }

    
    private void PerformBilling(CallContext context, List<ProductSelected> billingInstructions, Guid registrationApplicationId, InvoiceOrderType invoiceOrderType)
        {
            if (billingInstructions.Count == 0)
                return;

            using (var db = new Database())
            {
                var productIds = billingInstructions.Where(y => y.IsSelected).Select(z => z.ProductCode).ToArray();

                var products = db.BillableProducts.Where(x => productIds.Contains(x.ProductCode)).ToDictionary(y=>y.ProductCode);

                Invoice invoice = new Invoice();
                invoice.DueDate = DateTime.UtcNow;
                invoice.GenerationDate = DateTime.UtcNow;
                invoice.CreateUser = context.UserId;
                invoice.OrderId = registrationApplicationId.ToString();
                invoice.OrderType = invoiceOrderType;
                invoice.TennantId = context.TenantId;
                invoice.CreateDate = DateTime.UtcNow;
                invoice.ModifiedDate = null;

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
                            CreateUser = context.UserId,
                            CreateDate = DateTime.UtcNow
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
