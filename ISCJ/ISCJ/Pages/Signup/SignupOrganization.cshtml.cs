using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.Signup
{
    public class SignupOrganizationModel : BasePageModel
    {
        private readonly SignupManager _signupManager;

        public SignupOrganizationModel(SignupManager signupMgr)
        {
            _signupManager = signupMgr;
        }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
             
               var output = _signupManager.Signup(new OrganizationSignupInput()
                {
                    SignupEmail = Email,
                    OrganizationName = OrganizationName,
                    Password = Password
                });

               if (output.Success)
               {
                    ModelState.Clear();
               }
               else
               {
                    ModelState.AddModelError("OrganizationName", "Request failed: Error Code: " + output.FailureReason);
               }

            }

        }


        [BindProperty]
        [Required]
        public string Password { get; set; }


        [BindProperty]
        [Required]
        public string OrganizationName { get; set; }

        [BindProperty]
        public string Email { get; set; }
    }
}