using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.Registration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MA.Common.Models.api;
using MA.Core;

namespace BusinessLogic
{

  public class RegistrationManager
  {
    public CreateRegistrationApplicationOutput CreateRegistration(CallContext context, CreateRegistrationApplicationInput input)
    {
            using (TransactionScope scope = new TransactionScope())
            {
                var registrationApplicationId = SaveRegistrationApplication(context, input);
                PerformBilling(context, input, registrationApplicationId);
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


    public AddRegistrationOutput AddRegistrationToRegistrationApplication(CallContext context, AddRegistrationInput input)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            using (var db = new Database())
            {
                var enrollment = new Enrollment();
                var existingApplication =
                    db.RegistrationApplications.Where(x => x.ApplicationId == input.RegistrationApplicationId).Include(x=>x.Enrollments).Single();

                enrollment.ProgramId = existingApplication.ProgramId;
                enrollment.StudentContactId = input.StudentId;
                enrollment.IslamicSchoolGradeId = input.IslamicSchoolGrade;
                enrollment.PublicSchoolGradeId = input.PublicSchoolGrade;
                enrollment.CreateUser = context.UserId;
                enrollment.CreateDate = DateTime.UtcNow;
                //TODO: FOR Rafiq: Why Father and Mother id not getting populated automatically from parent.  Are relationships missing?
                enrollment.FatherId = existingApplication.FatherContactId;
                enrollment.MotherId = existingApplication.MotherContactId;
                
                existingApplication.Enrollments.Add(enrollment);
              
                db.SaveChanges();
            }

            //PerformBilling(context, input, registrationApplicationId);
            scope.Complete();
            return new AddRegistrationOutput()
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
                    FatherContactId = input.FatherId.Value,
                    MotherContactId = input.MotherId.Value,
                    ProgramId = input.ProgramId,
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
                        ProgramId = input.ProgramId,
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

    
    private void PerformBilling(CallContext context, CreateRegistrationApplicationInput input, Guid registrationApplicationId)
        {
            if (input.BillingInstructions.Count == 0)
                return;

            using (var db = new Database())
            {
                var productIds = input.BillingInstructions.Where(y => y.IsSelected).Select(z => z.ProductCode).ToArray();

                var products = db.BillableProducts.Where(x => productIds.Contains(x.ProductCode)).ToDictionary(y=>y.ProductCode);

                Invoice invoice = new Invoice();
                invoice.DueDate = DateTime.UtcNow;
                invoice.GenerationDate = DateTime.UtcNow;
                invoice.CreateUser = context.UserId;
                invoice.OrderId = registrationApplicationId.ToString();
                invoice.OrderType = InvoiceOrderType.RegistrationApplication;
                invoice.TennantId = context.TenantId;
                invoice.CreateDate = DateTime.UtcNow;
                invoice.ModifiedDate = null;

                invoice.InvoiceItems = new List<InvoiceItem>();
                foreach (var item in input.BillingInstructions.Where(x=>x.IsSelected))
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

    public Enrollment GetEnrollment(Guid registrationId)
    {
      using (var db = new Database())
      {
        return db.Enrollments.SingleOrDefault(x => x.EnrollmentId == registrationId);
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
              .Where(x => x.ProgramId == programId);

        if (registrationApplicationId != null)
        {
           query = query.Where(x => x.RegistrationApplicationId == registrationApplicationId.Value);
        }

        return query.ToList();
      }
    }



  }
}
