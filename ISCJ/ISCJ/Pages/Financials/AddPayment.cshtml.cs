using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.Payments;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ISCJ.Pages.Financials
{
    public class AddPaymentModel : BasePageModel
    {
        public AddPaymentModel()
        {

        }
        public void OnGet()
        {
            InitData();
            
        }

        private void InitData()
        {
            PaymentDate = DateTime.Now;
            PaymentMethod = PaymentMethod.Cash;
            CardBrand = CardBrand.Unknown;
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                PaymentsManager mgr = new PaymentsManager();
                mgr.CreatePayment(GetCallContext(), GetAddPaymentInput());
              Reset();
                
            }
            
        }

        public void OnPostReset()
        {
            Reset();

        }

        public void OnPostCancel()
        {
            Response.Redirect("Paymentlist");

        }

        private void Reset()
        {
        
            ModelState.Clear();
            PaymentAmount = 0;
            PaymentMethod = PaymentMethod.Cash;
            PaymentDate = DateTime.Now;
            PaymentNote = string.Empty;
            
        }
        private CreatePaymentInput GetAddPaymentInput()
        {
            CreatePaymentInput input = new CreatePaymentInput();
            input.PaymentDate = PaymentDate;
            input.PaymentAmount  = PaymentAmount;
            input.PaymentNote = PaymentNote;
            input.PaymentMadeByContactId = ContactId.Value;
            input.PaymentMethod = PaymentMethod;
            input.InvoiceId = InvoiceId;

            if (input.PaymentMethod == PaymentMethod.Check)
            {
                input.CheckPaymentDetail = new CheckPaymentDetail()
                {
                    CheckAccountNumber =  CheckAccountNumber,
                    CheckBankName = CheckBankName,
                    CheckCashDate = CheckCashableDate,
                    CheckNumber = CheckNumber,
                    NameOnCheck = NameOnCheck,
                    
                };
            }
            else if (input.PaymentMethod == PaymentMethod.CreditCard)
            {
                input.CardPaymentDetail = new CreditCardPaymentDetail()
                {
                    CardType = CardType,
                    ConfirmationNumber = GatewayConfirmationNumber,
                    Last4Digit = string.Empty,
                    GatewayName = GatewayName,
                    AuthorizationCode = AuthorizationCode,
                    CardBrand = CardBrand,
                    
                };
            }
            return input;
        }

        [BindProperty]
        public Guid? InvoiceId { get; set; }

        [BindProperty]
        public string AuthorizationCode { get; set; }


        [BindProperty]
        public string Name { get; set; }
        
        [BindProperty]
        [Required(ErrorMessage = "Payment must be attached to an entity.")]
        public Guid? ContactId { get; set; }
        
        [Required(ErrorMessage = "Payment Amount is required.")]
        [BindProperty]
        public decimal PaymentAmount { get; set; }

        [BindProperty]
        public string NameOnCheck { get; set; }


        [BindProperty]
        public string CheckAccountNumber { get; set; }

        [BindProperty]
        public DateTime CheckCashableDate { get; set; }


        [BindProperty]
        public string CheckBankName { get; set; }

[Required(ErrorMessage = "Payment description is required.")]
        [BindProperty]
        public string PaymentNote { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        [BindProperty]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage="Payment Method is required.", AllowEmptyStrings=false)]
        [BindProperty]
        public PaymentMethod PaymentMethod { get; set; }

         [BindProperty]
        public string CheckNumber { get; set; }

        [BindProperty]
        public CardBrand CardBrand { get; set; }

        [BindProperty]
        public CardType CardType { get; set; }

        [BindProperty]
        public string Last4Digits { get; set; }


        [BindProperty]
        public string GatewayConfirmationNumber { get; set; }

        [BindProperty]
        public string GatewayName { get; set; }

        [BindProperty]
        public string CardProcessorName { get; set; }
        

        public IEnumerable<SelectListItem> PaymentMethods
        {
            get {
                return ListService.GetPaymentMethods().Select(x => new SelectListItem(x.Value, x.Key));
            }
        }

        public IEnumerable<SelectListItem> CardTypes
        {
            get
            {
                return ListService.GetCardTypes().Select(x => new SelectListItem(x.Value, x.Key));
            }
        }

        public IEnumerable<SelectListItem> CardBrands
        {
            get
            {
                return ListService.GetCardBrands().Select(x => new SelectListItem(x.Value, x.Key));
            }
            
        }

    }
}