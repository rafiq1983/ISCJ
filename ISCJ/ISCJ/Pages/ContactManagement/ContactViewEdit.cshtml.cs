using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.ContactManagement
{
  public class ContactViewEditModel : PageModel
  {
    BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();
    public ContactViewEditModel()
    {
         
        }
    public void OnGet()
    {
      string editContactId = Request.Query["contactId"];
           
            if (string.IsNullOrEmpty(editContactId) == false)
      {                
                Contact = mgr.GetContact(Guid.Parse(editContactId));
      }
    }
    

 private bool Validate(Contact input, out List<string> errors)
    {
      errors = new List<string>();
      return true;
    }

        //TODO: Wire Post Method with Form Submit button.
        public IActionResult Post()
        {
               
        BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();
            if (ModelState.IsValid)
            {
                mgr.AddUpdateContact(Contact);

                Response.Redirect("ContactList");

            }

           return Page();
           
        }

        
    [BindProperty]
    public Contact Contact { get; set; }
    [BindProperty]
    public List<ContactType> ContactTypes { get { return ContactManager.GetContacTypes(); } }
    }

  

  }

 
