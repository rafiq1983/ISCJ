using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{

  class RegisterationManager
  {
    private static List<Registration> _Registrations = new List<Registration>();

    public string CreateRegistration(string programId, 
                      string studentId, string fatherId, string motherId,
                      string islamicSchoolGrade, string publicSchoolGrade)
    {
      ProductManager productMgr = new ProductManager();

      if(programId == "333e070b-58b5-4f28-91b0-c20c56072859")//islamic sunday school
      {
        var billing = productMgr.GetBillingItem("4aab2eb7-ae52-4101-bd6d-b9142829a50d");

        Invoice invoice = new Invoice();
        invoice.ResponsibleParty1 = fatherId;
        invoice.ResponsibleParty2 = motherId;
        invoice.ResponsibleParty3 = studentId;
        invoice.InvoiceItems.Add(billing);
        InvoiceManager invoiceMgr = new InvoiceManager();
        invoiceMgr.CreateInvoice(invoice);
      }

      Registration reg = new Registration();
      reg.FatherId = fatherId;
      reg.MotherId = motherId;
      reg.ProgramId = programId;
      reg.IslamicSchoolGrade = islamicSchoolGrade;
      reg.PublicSchoolGrade = publicSchoolGrade;
      reg.StudentId = studentId;
      reg.RegistrationId = Guid.NewGuid().ToString();

      _Registrations.Add(reg);

      return reg.RegistrationId;
    }

    public Registration GetRegistration(string registrationId)
    {
      return _Registrations.SingleOrDefault(x => x.RegistrationId == registrationId);
    }


  }
}
