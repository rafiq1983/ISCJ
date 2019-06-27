using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.Registration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MA.Common.Models.api;

namespace BusinessLogic
{

  public class RegistrationManager
  {
    private static List<Registration> _Registrations = new List<Registration>();


    public bool CreateRegistration(CreateRegistrationInput input)
    {
            using (TransactionScope ts = new TransactionScope())
            {
                SaveRegistrationApplication(input);
                CreateSingleRegistration(input);
                PerformBilling(input);
               return true;
               
            }

            return false;

    }


    private void CreateSingleRegistration(CreateRegistrationInput input)
    {
        using (var db = new Database())
        {
            for (int i = 0; i < input.StudentRegistration.Count; i++)
            {
                if (input.StudentRegistration[i].StudentId.HasValue)
                {
                    Registration reg = new Registration();
                    reg.FatherId = input.FatherId;
                    reg.MotherId = input.MotherId;
                    reg.ProgramId = input.ProgramId;
                    reg.IslamicSchoolGradeId = input.StudentRegistration[i].IslamicSchoolGrade;
                    reg.PublicSchoolGradeId = input.StudentRegistration[i].PublicSchoolGrade;
                    reg.StudentId = input.StudentRegistration[i].StudentId.Value;
                    db.Registrations.Add(reg);
                }
            }
        }
    }

    private void SaveRegistrationApplication(CreateRegistrationInput input)
                {
                    using (var db = new Database())
                    {
                      
                    }
                }

    private void PerformBilling(CreateRegistrationInput input)
        {
            using (var db = new Database())
            {
                Invoice invoice = null;

                if (input.AddSchoolMemberShipFee || input.AddStudentRegistrationFee)
                    invoice = new Invoice();

                if (invoice != null)
                {

                    invoice.DueDate = DateTime.Now;
                    invoice.GenerationDate = DateTime.Now;
                    invoice.InvoiceAmount = 0;

                    invoice.Description = "Yearly Palmyra Masjid Membership fee";
                }


                if (input.AddSchoolMemberShipFee)
                {
                    invoice.InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            InvoiceAmount = 365, Description = "School Yearly Membership Fee"
                        }
                    };

                    invoice.InvoiceAmount += 365;
                }


                db.Invoices.Add(invoice);

                for (int i = 0; i < input.StudentRegistration.Count; i++)
                {
                    if (input.StudentRegistration[i].StudentId.HasValue)
                    {
                        Registration reg = new Registration();
                        reg.FatherId = input.FatherId;
                        reg.MotherId = input.MotherId;
                        reg.ProgramId = input.ProgramId;
                        reg.IslamicSchoolGradeId = input.StudentRegistration[i].IslamicSchoolGrade;
                        reg.PublicSchoolGradeId = input.StudentRegistration[i].PublicSchoolGrade;
                        reg.StudentId = input.StudentRegistration[i].StudentId.Value;
                        db.Registrations.Add(reg);

                        if (input.AddStudentRegistrationFee)
                        {
                            using (ContactContext contactCtx = new ContactContext())
                            {

                                if (invoice.InvoiceItems == null)
                                    invoice.InvoiceItems = new List<InvoiceItem>();

                                var studentInfo = contactCtx.Contacts.Single(x =>
                                    x.Guid == input.StudentRegistration[i].StudentId);
                                string studentName = studentInfo.FirstName + " " + studentInfo.LastName;

                                invoice.InvoiceItems.Add(new InvoiceItem()
                                {
                                    InvoiceAmount = 20,
                                    Description = "Fee for " + studentName,
                                });

                                invoice.InvoiceAmount += 20;
                            }

                        }
                    }
                }
            }
        }

        public Guid CreateRegistration(Guid programId, 
                      Guid studentId, Guid fatherId, Guid motherId,
                      string islamicSchoolGrade, string publicSchoolGrade)
    {
      return Guid.Empty;
    }

    public Registration GetRegistration(Guid registrationId)
    {
      using (var db = new Database())
      {
        return db.Registrations.SingleOrDefault(x => x.RegistrationId == registrationId);
      }
    }

    public List<Registration> GetRegistrations()
    {
      using (var db = new Database())
      {
        return db.Registrations
          .Include(Registration => Registration.FatherContactInfo)
          .Include(registration=>registration.MotherContactInfo)
          .Include(Registration=>Registration.StudentContactInfo)
          .ToList();
      }
    }

    public List<Registration> GetRegistrations(Guid programId)
    {
      using (var db = new Database())
      {
        return db.Registrations
          .Include(Registration => Registration.FatherContactInfo)
          .Include(registration => registration.MotherContactInfo)
          .Include(Registration => Registration.StudentContactInfo)
          .Where(x => x.ProgramId == programId).ToList();
      }
    }



  }
}
