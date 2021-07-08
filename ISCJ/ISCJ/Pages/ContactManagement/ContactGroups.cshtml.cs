using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ContactManagement
{
    public class ContactGroupsModel : BasePageModel
    {
        public void OnGet()
        {

        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                ContactManager mgr = new ContactManager();

                mgr.CreateContactGroup(GetCallContext(), new CreateUpdateContactGroupInput()
                {
                    GroupName = GroupName,
                    GroupId = GroupId
                });

             
            }
        }
        
        public IEnumerable<ContactGroup> ContactGroups
        {
            get
            {
                ContactManager mgr = new ContactManager();

               return mgr.GetContactGroups(GetCallContext());
            }
        }

        public void OnPostCancel()
        {

        }

        public void OnPostReset()
        {

        }

        [BindProperty]
        [Required(ErrorMessage = "Group name is required.")]
        public string GroupName { get; set; }

        [BindProperty]
        public Guid? GroupId { get; set; }
    }
}