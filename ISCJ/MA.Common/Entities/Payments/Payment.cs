using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Payments
{
    public abstract class Payment : BaseEntity
    {
        public Guid PayorId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Guid FinancialAccountId { get; set; }
        public Guid TenantId { get; set; }
        public string PaymentNote { get; set; }
        public Guid? InvoiceId { get; set; }


    }
  public class CreditCardPayment:Payment
    {
        public string ConfirmationNumber { get; set; }
        public string GatewayName { get; set; }
        public string Last4Digits { get; set; }
        public string AuthorizationCode { get; set; }
        public CardBrand CardBrand { get; set; }
        public CardType CardType { get; set; }
    }

  public class CashPayment : Payment
  {

  }

    public class AllPayment
  {
      public Guid PaymentId { get; set; }
      public decimal PaymentAmount { get; set; }
      public PaymentMethod PaymentMethod { get; set; }
  }

    public enum PaymentMethod
    {
        Cash, CreditCard, Check
    }

    public enum CardBrand
    {
        Visa, MasterCard, Discovery, Unknown
    }

    public enum CardType
    {
        Credit, Debit, Unknown
    }

   

    public class CheckPayment:Payment
    {
        public string CheckNumber { get; set; }
        public string NameOnCheck { get; set; }
        public string CheckBankName { get; set; }
        public string CheckAccountNumber { get; set; }
        public DateTime CheckDate { get; set; }
        public DateTime CheckCashableDate { get; set; }

    }

}
