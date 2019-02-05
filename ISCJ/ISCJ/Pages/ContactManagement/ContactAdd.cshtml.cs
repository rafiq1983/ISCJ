using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
  public class ContactAddModel : PageModel
  {
    public void OnGet()
    {

    }

    //TODO: Wire Post Method with Form Submit button.
    public void onPost(CreateContactInput input)
    {
      List<string> errors = new List<string>();
       if(Validate(input, out errors))
      {
        BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();
        mgr.CreateContact(0, input);
      }
    }

    private bool Validate(CreateContactInput input, out List<string> errors)
    {
      errors = new List<string>();
      return true;
    }
  }

 
}
