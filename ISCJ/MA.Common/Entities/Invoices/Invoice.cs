using MA.Common.Models.api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    public decimal Amount { get; set; }

  }

    public class FinancialAccount:BaseEntity
    {
        public Guid FinancialAccountId { get; set; }
        public FinancialAccountType FinancialAccountType { get; set; }


        public string FinancialAccountName { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("FinancialAccountId")]
        public List<Invoice> Invoices { get; set; }
    }

    //TOOD: FOR Rafiq We have to use discrimnator column to return Contact FinancialAccount or Base FinancialAccount.
    public class ContactFinancialAccount:FinancialAccount
    {

    }
}
