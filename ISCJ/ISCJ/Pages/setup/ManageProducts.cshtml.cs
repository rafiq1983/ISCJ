using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Product;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class ManageProductsModel : BasePageModel
    {
        public void OnGet()
        {
           
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                ProductManager mgr = new ProductManager();

               var result = mgr.AddProduct(GetCallContext(), new AddProductInput()
                {
                    Description = ProductName,
                    EffectiveDate = DateTime.UtcNow,

                        ExpirationDate = DateTime.Parse("2999-01-01"),
                        IsActive =true,
                        Price= Price,
                        ProductCode = ProductCode

                });

               if (result.Success == false)
               {
                   ModelState.AddModelError("", result.FailureMessage);
               }
               else
               {
                   ClearFields();

               }
            }
        }

        public void OnPostReset()
        {
           
            ClearFields();

        }

        private void ClearFields()
        {
            ModelState.Clear();
            Price = 0;
            ProductCode = "";
            ProductName = "";

        }

        public IActionResult OnPostCancel()
        {
            RedirectToPageResult result = RedirectToPage("main");
            return result;
        }

        public List<BillableProduct> Products
        {
            get
            {
                ProductManager mgr = new ProductManager();
                return mgr.GetAllProducts(GetCallContext());
            }
        }

        [BindProperty]
        [Required]
        public decimal Price { get; set; }
        [BindProperty]
        [Required]
        public string ProductName { get; set; }
        [BindProperty]
        [Required]
        public string ProductCode { get; set; }

        
        
    }
}