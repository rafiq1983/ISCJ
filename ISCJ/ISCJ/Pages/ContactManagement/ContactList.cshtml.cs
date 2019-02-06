﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
    public class ContactListModel : PageModel
    {
    private List<Contact> _contacts;

    public void OnGet()
    {
      ContactManager mgr = new ContactManager();
      _contacts = mgr.GetContacts(1, 1, 1000);

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

    public List<Contact> Contacts
    {
      get
      {
        return _contacts;
      }
    }
    }
}
