using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Payments
{
  public class Payment:BaseEntity
    {
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Guid PayerId { get; set; }
        public Guid FinancialAccountId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }

    public enum PaymentMethod
    {
        Cash, CreditCard, Check
    }

    //TODO: Rafiq: Needs to specify discriminator column.
    public class CheckPayment:Payment
    {
        public string CheckNumber { get; set; }
        public string NameOnCheck { get; set; }
        public string CheckBankName { get; set; }
        public string CheckAccountNumber { get; set; }
        public DateTime CheckDate { get; set; }

    }

}
