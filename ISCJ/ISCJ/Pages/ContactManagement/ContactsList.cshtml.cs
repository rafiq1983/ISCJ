using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ISCJ.Pages.ContactManagement
{
    public class ContactsListModel : BasePageModel
    {
    private List<Contact> _contacts;
    ContactManager _mgr = new ContactManager();

        public ContactsListModel(IConfiguration configSection)
    {

    }

    public void OnGet()
    {
     
      _contacts = _mgr.GetContacts(GetCallContext(), 1, 1, 1000);

    }

    [BindProperty]
    public Guid GroupId { get; set; }
    public IEnumerable<ContactGroup> ContactGroups
    {
        get
        {
          
           return _mgr.GetContactGroups(base.GetCallContext());
           
        }

    }

    public void OnPost(string btnAction)
    {
        if (GroupId != Guid.Empty)
        {
            _contacts = _mgr.GetContactsByGroup(GetCallContext(), GroupId);
        }
        else
        {
            _contacts = _mgr.GetContacts(GetCallContext(), 1, 1, 1000);

        }
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
