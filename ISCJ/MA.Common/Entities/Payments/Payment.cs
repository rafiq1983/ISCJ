using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Payments
{
    public abstract class Payment : BaseEntity
    {
        public Guid PayerId { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
      
        public Guid FinancialAccountId { get; set; }

    }
  public class CashPayment:Payment
    {  

    }

    public enum PaymentMethod
    {
        Cash, CreditCard, Check
    }

    public class CheckPayment:Payment
    {
        public string CheckNumber { get; set; }
        public string NameOnCheck { get; set; }
        public string CheckBankName { get; set; }
        public string CheckAccountNumber { get; set; }
        public DateTime CheckDate { get; set; }

    }

}
