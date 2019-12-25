using MA.Common.Models.api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MA.Common.Entities.Invoices
{
    public class InvoiceType : BaseEntity
    {
        public Guid InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public Guid TenantId { get; set; }
        public string ShortDescription { get; set; }
    }

    public class Invoice:BaseEntity
  {
    public Guid InvoiceId { get; set; }
    public decimal InvoiceAmount { get; set; }
    public Guid TennantId { get; set; }
    public Guid FinancialAccountId { get; set; }
    public Guid? InvoiceTypeId { get; set; }
    public DateTime DueDate { get; set; }
    public Guid? ResponsiblePartyId { get; set; }
    public string Description { get; set; }

    public DateTime GenerationDate { get; set; }
    [Display(Name = "Paid Amount")]
    [DataType(DataType.Currency)]
    public decimal TotalPaid { get; set; }
    public bool IsPaid { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; }

        public FinancialAccount FinancialAccount;

        public string ReferenceId { get; set; }
        public ReferenceType ReferenceType { get; set; }

  }

  public class InvoiceItem:BaseEntity
  { 
    public Guid ItemId { get; set; }

    public Guid InvoiceId { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }
    
    public int Quantity { get; set; }

    public Guid TenantId { get; set; }

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

    public enum ReferenceType
    {
        Other,RegistrationApplication, Enrollment, MembershipCreation,AdHocInvoiceAttachedToContact
    }
}
