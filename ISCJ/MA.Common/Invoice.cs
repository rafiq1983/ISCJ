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

    public List<Product> InvoiceItems { get; set; }

  }

}
