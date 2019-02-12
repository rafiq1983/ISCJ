using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic;

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
    public List<ContactType> ContactTypes
    {
      get
      {
        return ContactManager.GetContacTypes();
      }
    }

 private bool Validate(Contact input, out List<string> errors)
    {
      errors = new List<string>();
      return true;
    }

          //TODO: Wire Post Method with Form Submit button.
    public void OnPost()
    {
      //TODO: How to do model binding.
      List<string> errors = new List<string>();
       if(Validate(Contact, out errors))
      {

       BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();

       mgr.AddUpdateContact(Contact);

        Response.Redirect("ContactList");
      }

    }

        
    [BindProperty]
    public Contact Contact
    {
      get;
      set;
    }

    }

  

  }

 
