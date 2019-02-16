using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
  
  public class InvoiceManager
  {
    private static List<Invoice> _Invoices = new List<Invoice>();

    public List<Invoice> GetInvoices()
    {
      using (Database db = new Database())
      {
        return db.Invoices.ToList();
      }
    }

    public Invoice GetInvoice(Guid invoiceId)
    {
      return _Invoices.SingleOrDefault(x => x.InvoiceId == invoiceId);
    }

    
  }

 
}
