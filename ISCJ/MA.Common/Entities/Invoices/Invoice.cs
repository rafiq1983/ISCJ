using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Invoices
{
  public class Invoice
  {
    public Guid InvoiceId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DueDate { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; }
  }

  public class InvoiceItem
  {
    
    public Guid ItemId { get; set; }

    public Guid InvoiceId { get; set; }
    public string Description { get; set; }

    public decimal Amount { get; set; }

  }
}
