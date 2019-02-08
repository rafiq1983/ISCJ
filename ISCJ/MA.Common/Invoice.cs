using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common
{
  public class Invoice
  {
    
    public string InvoiceId { get; set; }
    public string ResponsibleParty1 { get; set; }

    public string ResponsibleParty2 { get; set; }

    public string ResponsibleParty3 { get; set; }

    public string ResponsibleParty4 { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; }

    public DateTime DueDate { get; set; }
    public DateTime GenerationDate { get; set; }

    public decimal Amount { get; set; }

  }

  public class InvoiceItem
  {
    public string ItemId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }

    public string InvoiceCategory { get; set; }
  }

}
