using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class AddUserModel : BasePageModel
    {
        private readonly SignupManager _signupManager;

        public AddUserModel(SignupManager mgr)
        {
            _signupManager = mgr;
        }
        public void OnGet()
        {
            UserManager mgr = new UserManager();
            User u = mgr.GetUsersByUserName(GetCallContext().UserLoginName);
           var userTenant = u.UserTenants.Single(x => x.TenantId == GetCallContext().TenantId);

           if(userTenant.RoleCd!="ADMIN")
           {
               throw new Exception("You are not allowed access to this page");
           }
            Users = GetUsers();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                bool output = _signupManager.AddUserToOrganization(GetCallContext(), Email, Role);
                if(output==false)
                    ModelState.AddModelError("", "Failed");
            }

            Users = GetUsers();

        }

        private List<UserModel> GetUsers()
        {
            UserManager mgr = new UserManager();
           var users =  mgr.GetTenantUsers(GetCallContext());
           return users.Select(x => new UserModel()
           {
               Email = x.UserName,
               FirstName = x.Contact.FirstName,
               LastName = x.Contact.LastName,
               Role = "TBD"
           }).ToList();
        }
        public List<UserModel> Users { get; set; }

        [BindProperty][Required] public string Role { get; set; }

        [BindProperty]
        [Required]
        public string Email { get; set; }


        
    }

    public class UserModel
    {
        public string FirstName;
        public string LastName;
        public string Email;
        public string Role;
    }
}