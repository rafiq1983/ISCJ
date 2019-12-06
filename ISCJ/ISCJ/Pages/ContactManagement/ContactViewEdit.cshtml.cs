using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.ContactManagement
{
  public class ContactViewEditModel : BasePageModel
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
                var contact = mgr.GetContact(Guid.Parse(editContactId));

                Contact = new SaveContactInput()
                {
                    Apt = contact.Apt,
                    CellPhone = contact.CellPhone,
                    City = contact.City,
                    CompanyName = contact.CompanyName,
                    ContactType = contact.ContactType,
                    DOB = contact.DOB,
                    Email = contact.Email,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName, Guid = contact.Guid, Gender = contact.Gender,
                    IsParent = contact.IsParent,
                    HomePhone = contact.HomePhone,
                    MiddleName = contact.MiddleName,
                    Organization = contact.Organization,
                    State = contact.State,
                    StreetAddress = contact.StreetAddress,
                    ZipCode = contact.ZipCode
                };
      }
    }
    
       //TODO: Wire Post Method with Form Submit button.
        [HttpPost]
        public IActionResult OnPost()
        {
               
        BusinessLogic.ContactManager mgr = new BusinessLogic.ContactManager();
            if (ModelState.IsValid)
            {
                string editContactId = Request.Query["contactId"];
                if (string.IsNullOrEmpty(editContactId) == false)
                    Contact.Guid = Guid.Parse(editContactId);

                mgr.AddUpdateContact(GetCallContext(), Contact);
             
                Response.Redirect("Contacts");

            }

           return Page();
           
        }

        
    [BindProperty]
    public SaveContactInput Contact { get; set; }
    public List<ContactType> ContactTypes { get { return ContactManager.GetContacTypes(); } }

    }
     
  }

 
