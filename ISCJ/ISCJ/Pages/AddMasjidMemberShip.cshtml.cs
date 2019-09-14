using System;
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
    public class AddMasjidMemberShipModel : PageModel
    {
        private MasjidMembershipManager mgr = new MasjidMembershipManager();
        private ProductManager productMgr = new ProductManager();


        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "234234", "askfj", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }


        public List<ContactType> ContactTypes { get { return ContactManager.GetContacTypes(); } }


        public AddMasjidMemberShipModel()
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
                mgr.CreateMasjidMembership(GetCallContext(), new CreateMasjidMembershipInput()
                {
                    ContactId = ContactId.GetValueOrDefault(Guid.Empty),
                    BillingInstructions = BillingInstructions.Where(x => x.IsSelected == true).ToList()
                });
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