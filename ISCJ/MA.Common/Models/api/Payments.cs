using MA.Common.Entities.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
  public class CreatePaymentInput
    {
        public Guid FinancialAccountId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal PaymentAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public CheckPaymentDetail CheckPaymentDetail { get; set; }
        
    }

    public class CheckPaymentDetail
    {
        public string CheckNumber { get; set; }
        public DateTime CheckDate { get; set; }

        public string NameOnCheck { get; set; }
        public string CheckBankName { get; set; }

        public string CheckAccountNumber { get; set; }
    }

    public class CreatePaymentOutput
    {
        public Guid PaymentId {get;set;}
        public bool Success { get; set; }
    }
}
