﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class AddMasjidMemberShipModel : BasePageModel
    {
        private MasjidMembershipManager mgr = new MasjidMembershipManager();
        private ProductManager productMgr = new ProductManager();


        
        public List<ContactType> ContactTypes { get { return ContactManager.GetContacTypes(); } }


        public AddMasjidMemberShipModel()
        {
        }
        public void OnGet()
        {

            Products = productMgr.GetAllProducts(GetCallContext());

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                mgr.CreateMasjidMembership(GetCallContext(), new CreateMasjidMembershipInput()
                {
                    ContactId = ContactId.GetValueOrDefault(Guid.Empty),
                    BillingInstructions = BillingInstructions.Where(x => x.IsSelected == true).ToList(),
                    EffectiveDate = EffectiveDate,
                    ExpirationDate = ExpirationDate
                });

                Response.Redirect("Financials/InvoiceList");
            }
            else
            {

                Products = productMgr.GetAllProducts(GetCallContext());
            }
        }


        [BindProperty] public string ContactName { get; set; }
        [Required(AllowEmptyStrings=false, ErrorMessage = "Contact must be selected.")]
        [BindProperty] public Guid? ContactId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Effective date must be selected.")]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
      
        public DateTime EffectiveDate { get; set; } = DateTime.Now;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Expiration date must be selected.")]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddYears(1);
        [BindProperty]
        public List<ProductSelected> BillingInstructions { get; set; }
        [BindProperty]
        public List<MA.Common.Entities.Product.BillableProduct> Products { get; set; }


    }
}