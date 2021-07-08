using MA.Common.Entities.Payments;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class PaymentsManager
    {
        public CreatePaymentOutput CreatePayment(CallContext context, CreatePaymentInput input)
        {
            InvoiceManager mgr = new InvoiceManager();

            using (TransactionScope scope = new TransactionScope())
            {

                using (var db = new Database())
                {

                    if (input.PaymentMethod == PaymentMethod.Check)
                    {
                        var pmt = new CheckPayment();
                        pmt.FinancialAccountId = input.FinancialAccountId;
                        pmt.PayorId = input.PaymentMadeByContactId;
                        pmt.PaymentAmount = input.PaymentAmount;
                        pmt.PaymentDate = input.PaymentDate;
                        pmt.PaymentId = Guid.NewGuid();
                        pmt.CheckAccountNumber = input.CheckPaymentDetail.CheckAccountNumber;
                        pmt.CheckBankName = input.CheckPaymentDetail.CheckBankName;
                        pmt.CheckDate = input.CheckPaymentDetail.CheckCashDate;
                        pmt.CheckNumber = input.CheckPaymentDetail.CheckNumber;
                        pmt.NameOnCheck = input.CheckPaymentDetail.NameOnCheck;
                        pmt.PaymentNote = input.PaymentNote;
                        pmt.CreateDate = DateTime.UtcNow;
                        pmt.CreateUser = context.UserLoginName;
                        pmt.TenantId = context.TenantId.Value;
                        pmt.InvoiceId = input.InvoiceId;
                        pmt.CheckCashableDate = input.CheckPaymentDetail.CheckCashDate;
                        db.CheckPayments.Add(pmt);

                        db.SaveChanges();

                        if (pmt.InvoiceId.HasValue==true)
                            mgr.AddInvoicePayment(context, pmt.InvoiceId.Value, pmt.PaymentAmount);
                        scope.Complete();
                        return new CreatePaymentOutput() {PaymentId = pmt.PaymentId};
                    }
                    else if (input.PaymentMethod == PaymentMethod.CreditCard)
                    {
                        var pmt = new CreditCardPayment();
                        pmt.FinancialAccountId = input.FinancialAccountId;
                        pmt.PayorId = input.PaymentMadeByContactId;
                        pmt.PaymentAmount = input.PaymentAmount;
                        pmt.PaymentDate = input.PaymentDate;
                        pmt.PaymentId = Guid.NewGuid();
                        pmt.CardType = input.CardPaymentDetail.CardType;
                        pmt.GatewayName = input.CardPaymentDetail.GatewayName;
                        pmt.ConfirmationNumber = input.CardPaymentDetail.ConfirmationNumber;
                        pmt.AuthorizationCode = input.CardPaymentDetail.AuthorizationCode;
                        pmt.Last4Digits = input.CardPaymentDetail.Last4Digit;
                        pmt.PaymentNote = input.PaymentNote;
                        pmt.CreateDate = DateTime.UtcNow;
                        pmt.CreateUser = context.UserLoginName;
                        pmt.TenantId = context.TenantId.Value;
                        pmt.InvoiceId = input.InvoiceId;
                        db.CreditCardPayments.Add(pmt);
                        db.SaveChanges();

                        if (pmt.InvoiceId.HasValue == true)
                            mgr.AddInvoicePayment(context, pmt.InvoiceId.Value, pmt.PaymentAmount);

                        scope.Complete();
                        return new CreatePaymentOutput() {PaymentId = pmt.PaymentId};
                    }
                    else
                    {
                        var pmt = new CashPayment();
                        pmt.InvoiceId = input.InvoiceId;
                        pmt.FinancialAccountId = input.FinancialAccountId;
                        pmt.PayorId = input.PaymentMadeByContactId;
                        pmt.PaymentAmount = input.PaymentAmount;
                        pmt.PaymentDate = input.PaymentDate;
                        pmt.PaymentId = Guid.NewGuid();
                        pmt.CreateDate = DateTime.UtcNow;
                        pmt.CreateUser = context.UserLoginName;
                        pmt.TenantId = context.TenantId.Value;
                        pmt.PaymentNote = input.PaymentNote;
                        db.CashPayments.Add(pmt);
                        db.SaveChanges();

                        if (pmt.InvoiceId.HasValue == true)
                            mgr.AddInvoicePayment(context, pmt.InvoiceId.Value, pmt.PaymentAmount);
                        scope.Complete();
                        return new CreatePaymentOutput() {PaymentId = pmt.PaymentId};
                    }
                }

               
            }
        }

        public List<CheckPayment> GetCheckPayments(CallContext context)
        {
            using (var db = new Database())
            {   
                return db.CheckPayments.ToList();
            }
        }

        public List<CashPayment> GetCashPayments(CallContext context)
        {
            using (var db = new Database())
            {   
                return db.CashPayments.ToList();

            }
        }
        public List<CreditCardPayment> GetCreditCardPayments(CallContext context)
        {
            using (var db = new Database())
            {
                return db.CreditCardPayments.ToList();

            }
        }

        public List<AllPayment> GetAllPayments(CallContext context)
        {
            using (var db = new Database())
            {
                return db.AllPaymentIds
                    .FromSql(
                        @"SELECT PaymentId, PaymentAmount, 'Cash' as PaymentMethod, InvoiceId, PayorId, PaymentDate from CashPayment where TenantId=@tenantId
                    union SELECT PaymentId, PaymentAmount, 'Check' as PaymentMethod,  InvoiceId, PayorId, PaymentDate from CheckPayment  where TenantId = @tenantId
                    union SELECT PaymentId, PaymentAmount,  'CreditCard' as PaymentMethod, InvoiceId,  PayorId, PaymentDate from CreditCardPayment where tenantId=@tenantId",
                        new SqlParameter(){ParameterName="@tenantId", Value=context.TenantId})
                    .ToList();
            }
        }
    }
}
