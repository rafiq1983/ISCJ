using MA.Common.Entities.Payments;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace BusinessLogic
{
    public class PaymentsManager
    {
        public CreatePaymentOutput CreatePayment(CallContext context, CreatePaymentInput input)
        {
          
            using (var db = new Database())
            {
                
                if(input.PaymentMethod ==  PaymentMethod.Check)
                {
                    var pmt = new CheckPayment();
                    pmt.FinancialAccountId = input.FinancialAccountId;
                    pmt.PayerId = Guid.Empty;
                    pmt.PaymentAmount = input.PaymentAmount;
                    pmt.PaymentDate = input.PaymentDate;
                    pmt.PaymentId = Guid.NewGuid();
                    //pmt.PaymentMethod = input.PaymentMethod;
                    pmt.CheckAccountNumber = input.CheckPaymentDetail.CheckAccountNumber;
                    pmt.CheckBankName = input.CheckPaymentDetail.CheckBankName;
                    pmt.CheckDate = input.CheckPaymentDetail.CheckDate;
                    pmt.CheckNumber = input.CheckPaymentDetail.CheckNumber;
                    pmt.NameOnCheck = input.CheckPaymentDetail.NameOnCheck;
                    pmt.CreateDate = DateTime.UtcNow;
                    pmt.CreateUser = context.UserId;
                    db.CheckPayments.Add(pmt);
                    db.SaveChanges();
                    return new CreatePaymentOutput() { PaymentId = pmt.PaymentId };
                }
                else if(input.PaymentMethod == PaymentMethod.Cash)
                {
                    var pmt = new CashPayment();
                    pmt.FinancialAccountId = input.FinancialAccountId;
                    pmt.PayerId = Guid.Empty;
                    pmt.PaymentAmount = input.PaymentAmount;
                    pmt.PaymentDate = input.PaymentDate;
                    pmt.PaymentId = Guid.NewGuid();
                    //pmt.PaymentMethod = input.PaymentMethod;
                    pmt.CreateDate = DateTime.UtcNow;
                    pmt.CreateUser = context.UserId;
                    db.CashPayments.Add(pmt);
                    db.SaveChanges();
                    return new CreatePaymentOutput() { PaymentId = pmt.PaymentId };
                }
                else
                {
                    return new CreatePaymentOutput() { Success = false };
                }
            }
        }

        public List<CheckPayment> GetPayments(CallContext context)
        {
            using (var db = new Database())
            {
                
                return db.CheckPayments.ToList();
            }
        }

        public List<CashPayment> GetCashPaments(CallContext context)
        {
            using (var db = new Database())
            {
                return db.CashPayments.ToList();
            }
        }
    }
}
