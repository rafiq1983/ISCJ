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
      return _Invoices;
    }

    public Invoice GetInvoice(string invoiceId)
    {
      return _Invoices.SingleOrDefault(x => x.InvoiceId == invoiceId);
    }

    public string CreateInvoice(Invoice invoice)
    {
      if(string.IsNullOrEmpty(invoice.InvoiceId))
      {
        invoice.InvoiceId = Guid.NewGuid().ToString();
        _Invoices.Add(invoice);
      }
      else
      {
        var index = _Invoices.IndexOf(_Invoices.Single(x => x.InvoiceId == invoice.InvoiceId));
        _Invoices[index] = invoice;
        
      }

      return invoice.InvoiceId;
    }


  }

 
}
