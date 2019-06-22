using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Invoices
{
  public class Invoice:BaseEntity
  {
    public Guid InvoiceId { get; set; }
    public decimal InvoiceAmount { get; set; }

    public Guid FinancialAccountId { get; set; }

    public DateTime DueDate { get; set; }

    public string Description { get; set; }

    public DateTime GenerationDate { get; set; }

    public bool IsPaid { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; }

        public FinancialAccount FinancialAccount;

  }

  public class InvoiceItem
  { 
    public Guid ItemId { get; set; }

    public Guid InvoiceId { get; set; }

    public string Description { get; set; }

    public decimal InvoiceAmount { get; set; }

  }

    public class FinancialAccount
    {
        public Guid FinancialAccountId { get; set; }
        public Guid FinancialEntityId { get; set; }
        public string FinancialEntityType { get; set; }
    }

    //TOOD: FOR Rafiq We have to use discrimnator column to return Contact FinancialAccount or Base FinancialAccount.
    public class ContactFinancialAccount:FinancialAccount
    {

    }
}
