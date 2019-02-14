using MA.Common;
using MA.Common.Entities.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{

  public class RegistrationManager
  {
    private static List<Registration> _Registrations = new List<Registration>();


    public bool CreateRegistration(CreateRegistrationInput input)
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
        db.SaveChanges();
      }

      return true;
    }


    public Guid CreateRegistration(Guid programId, 
                      Guid studentId, Guid fatherId, Guid motherId,
                      string islamicSchoolGrade, string publicSchoolGrade)
    {
      ProductManager productMgr = new ProductManager();

      if(programId == Guid.Parse("333e070b-58b5-4f28-91b0-c20c56072859"))//islamic sunday school
      {
        var billing = productMgr.GetBillingItem("4aab2eb7-ae52-4101-bd6d-b9142829a50d");

        Invoice invoice = new Invoice();
        invoice.ResponsibleParty1 = fatherId.ToString();
        invoice.ResponsibleParty2 = motherId.ToString();
        invoice.ResponsibleParty3 = studentId.ToString();
        invoice.InvoiceItems.Add(new InvoiceItem() { ItemId = Guid.NewGuid().ToString(), Amount = billing.Amount, Description = billing.Description, InvoiceCategory = "School Registration" });
        InvoiceManager invoiceMgr = new InvoiceManager();
        invoiceMgr.CreateInvoice(invoice);
      }

      Registration reg = new Registration();
      reg.FatherId = fatherId;
      reg.MotherId = motherId;
      reg.ProgramId = programId;
      reg.IslamicSchoolGradeId = islamicSchoolGrade;
      reg.PublicSchoolGradeId = publicSchoolGrade;
      reg.StudentId = studentId;

      using (var db = new Database())
      {
        db.Registrations.Add(reg);
        db.SaveChanges();
      }
        
      return reg.RegistrationId;
    }

    public Registration GetRegistration(Guid registrationId)
    {
      using (var db = new Database())
      {
        return db.Registrations.SingleOrDefault(x => x.RegistrationId == registrationId);
      }
    }

    public List<RegistrationDetail> GetRegistrations()
    {
      using (var db = new Database())
      {
        return db.Registrations.ToList().Select(x => new RegistrationDetail() { RegistrationId = x.RegistrationId }).ToList();
      }
    }


  }
}
