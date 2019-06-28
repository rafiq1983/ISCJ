using MA.Common.Entities.Payments;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
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
                    pmt.PaymentMethod = input.PaymentMethod;
                    pmt.CheckAccountNumber = input.CheckPaymentDetail.CheckAccountNumber;
                    pmt.CheckBankName = input.CheckPaymentDetail.CheckBankName;
                    pmt.CheckDate = input.CheckPaymentDetail.CheckDate;
                    pmt.CheckNumber = input.CheckPaymentDetail.CheckNumber;
                    pmt.CreateDate = DateTime.UtcNow;
                    pmt.CreateUser = context.UserId;
                    db.Payments.Add(pmt);
                    db.SaveChanges();
                    return new CreatePaymentOutput() { PaymentId = pmt.PaymentId };
                }
                else if(input.PaymentMethod == PaymentMethod.Cash)
                {
                    var pmt = new Payment();
                    pmt.FinancialAccountId = input.FinancialAccountId;
                    pmt.PayerId = Guid.Empty;
                    pmt.PaymentAmount = input.PaymentAmount;
                    pmt.PaymentDate = input.PaymentDate;
                    pmt.PaymentId = Guid.NewGuid();
                    pmt.PaymentMethod = input.PaymentMethod;
                    pmt.CreateDate = DateTime.UtcNow;
                    pmt.CreateUser = context.UserId;
                    db.Payments.Add(pmt);
                    db.SaveChanges();
                    return new CreatePaymentOutput() { PaymentId = pmt.PaymentId };
                }
                else
                {
                    return new CreatePaymentOutput() { Success = false };
                }
            }
        }
    }
}
