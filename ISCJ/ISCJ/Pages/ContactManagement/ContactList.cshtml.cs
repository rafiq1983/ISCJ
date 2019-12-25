using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ISCJ.Pages.ContactManagement
{
    public class ContactListModel : BasePageModel
    {
    private List<Contact> _contacts;

    public ContactListModel(IConfiguration configSection)
    {
      string s = configSection["ConnectionStrings:Primary"];
    }

    public void OnGet()
    {
     
      ContactManager mgr = new ContactManager();
      _contacts = mgr.GetContacts(GetCallContext(), 1, 1, 1000);

    }

    public void OnPost(string btnAction)
    {
      if (btnAction == "Details")
      {
        string contactId = Request.Form["contactId"];
        Response.Redirect("ContactViewEdit?contactId=" + contactId);
         }
      else if(btnAction=="Add New")
      {
        Response.Redirect("ContactViewEdit?mode=new");
       
      }

      _contacts = new List<Contact>();
    }

    [BindProperty]
    public List<Contact> Contacts
    {
      get
      {
        return _contacts;
      }
    }
    }
}
