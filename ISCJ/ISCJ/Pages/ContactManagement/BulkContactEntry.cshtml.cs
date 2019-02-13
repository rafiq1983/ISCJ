using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
  public class BulkContactEntryModel : PageModel
  {
    public BulkContactEntryModel()
    {
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
    }

    [BindProperty]
    public List<Contact> BulkContacts
    {
        get;set;
    }
    }


}
