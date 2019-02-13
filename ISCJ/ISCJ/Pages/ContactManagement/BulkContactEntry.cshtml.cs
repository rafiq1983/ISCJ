using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ISCJ.Pages.ContactManagement
{

  public class BulkContactEntryModel : PageModel
  {
    private List<SelectListItem> _GenderList = null;
    public BulkContactEntryModel()
    {
      _GenderList = new List<SelectListItem>();
      _GenderList.Add(new SelectListItem() { Text = "Male", Value = "1" });
      _GenderList.Add(new SelectListItem() { Text = "Female", Value = "2" });

      BulkContacts = new List<Contact>();
      for(int i=0;i<20;i++)
      {
        BulkContacts.Add(new Contact());
      }
    }
    public void OnGet()
    {
     
    }

    public List<ContactType> ContactTypes
    {
      get
      {
        return ContactManager.GetContacTypes();
      }
    }

    public void OnPost()
    {
      ContactManager mgr = new ContactManager();
      
      mgr.SaveBulkContact(BulkContacts.ToList());

      Response.Redirect("ContactList");
    }

    [BindProperty]
    public List<Contact> BulkContacts
    {
        get;set;
    }

    public List<SelectListItem> GenderList
    {
      get
      {
        return _GenderList;
      }
      
    }
    }


}
