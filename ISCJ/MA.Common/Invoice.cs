using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common
{
  public class Invoice
  {
    
    public Guid InvoiceId { get; set; }
    public Guid ResponsibleParty1Id { get; set; }

    public Guid ResponsibleParty2Id { get; set; }


    public List<InvoiceItem> InvoiceItems { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime GenerationDate { get; set; }

    public decimal InvoiceAmount { get; set; }

    public string Description { get; set; }

    public bool IsPaid { get; set; }

  }

  public class InvoiceItem
  {
    public string ItemId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal SalesTax { get; set; }
    public Guid ItemTypeId { get; set; }
  }

}
