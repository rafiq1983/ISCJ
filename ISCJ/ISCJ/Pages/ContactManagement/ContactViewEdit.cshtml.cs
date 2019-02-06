using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
  public class ContactViewEditModel : PageModel
  {
    BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();
    public ContactViewEditModel()
    {
      Contact = new Contact();
    }
    public void OnGet()
    {
      string editContactId = Request.Query["contactId"];

      if(string.IsNullOrEmpty(editContactId)==false)
      {
        Contact = mgr.GetContact(editContactId);
      }
    }



    //TODO: Wire Post Method with Form Submit button.
    public void OnPost()
    {
      //TODO: How to do model binding.
      List<string> errors = new List<string>();
       if(Validate(Contact, out errors))
      {

        BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();

        mgr.AddUpdateContact(0, Contact);

        Response.Redirect("ContactList");
      }

    }

    private bool Validate(Contact input, out List<string> errors)
    {
      errors = new List<string>();
      return true;
    }

    [BindProperty]
    public Contact Contact
    {
      get;
      set;
    }
  }

 
}
