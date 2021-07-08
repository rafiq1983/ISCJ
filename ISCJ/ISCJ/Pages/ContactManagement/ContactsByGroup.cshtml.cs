using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
    public class ContactsByGroupModel : BasePageModel
    {
        private List<Contact> _contacts;
        private Guid _groupId;

        public void OnGet([FromRoute] Guid groupId)
        {
            _groupId = groupId;
            ContactManager mgr = new ContactManager();
            _contacts = mgr.GetContactsByGroup(GetCallContext(),groupId);

        }

        public void OnPost(string btnAction)
        {
            if (btnAction == "Details")
            {
                string contactId = Request.Form["contactId"];
                Response.Redirect("ContactViewEdit?contactId=" + contactId);
            }
            else if (btnAction == "Add New")
            {
                Response.Redirect("ContactViewEdit?mode=new");

            }

            _contacts = new List<Contact>();
        }

        public string GroupName
        {
            get
            {
                ContactManager mgr = new ContactManager();
                return mgr.GetContactGroup(GetCallContext(), _groupId).GroupName;
            }
        }
        
        public List<Contact> Contacts
        {
            get
            {
                return _contacts;
            }
        }

    }
}