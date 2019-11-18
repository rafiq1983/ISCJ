using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.Financials
{
    public class AddInvoiceModel : PageModel
    {
        ProgramManager mgr = new ProgramManager();
        private ProductManager productMgr = new ProductManager();

        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "234234", "askfj", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }
        public AddInvoiceModel()
        {

            Products = productMgr.GetAllProducts(GetCallContext());
        }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                InvoiceManager invoiceMgr = new InvoiceManager();
                invoiceMgr.CreateInvoice(GetCallContext(), GetCreateInvoiceInput());
                Response.Redirect("InvoiceList");
            }
        }

        private CreateInvoiceInput GetCreateInvoiceInput()
        {
            CreateInvoiceInput input = new CreateInvoiceInput();
            input.InvoiceTypeId = InvoiceType;
            input.Amount = InvoiceAmount;
            input.Description = InvoiceDescription;
            input.ReferenceId = ReferenceId;
            return input;
        }

        [BindProperty]
        public string Name { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Invoice must be attached to an entity.")]
        public string ReferenceId { get; set; }

        [BindProperty]
        public List<ProductSelected> BillingInstructions { get; set; }

        [BindProperty]
        public List<MA.Common.Entities.Product.BillableProduct> Products { get; set; }

        [Required(ErrorMessage = "Invoice Amount is required.")]
        [BindProperty]
        public decimal InvoiceAmount { get; set; }

        [Required(ErrorMessage = "Invoice description is required.")]
        [BindProperty]
        public string InvoiceDescription { get; set; }

        [Required(ErrorMessage = "Invoice Due Date is required.")]
        [BindProperty]
        public DateTime InvoiceDueDate { get; set; }
        [Required(ErrorMessage="Invoice Type is required.", AllowEmptyStrings=false)]
        [BindProperty]
        public string InvoiceType { get; set; }

        public IEnumerable<SelectListItem> InvoiceTypes
        {
            get {
                InvoiceManager invoiceMgr = new InvoiceManager();
                return invoiceMgr.GetInvoiceTypes(GetCallContext()).Select(x => new SelectListItem(x.InvoiceTypeName, x.InvoiceTypeId.ToString()));

            }
        }


    }
}