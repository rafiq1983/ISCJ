﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ISCJ.Pages.Signup
{
    public class SignupUserModel : BasePageModel
    {
        public void OnGet()
        {

        }

       

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                if (Password != ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Passwords do not match");
                }

                UserManager mgr = new UserManager();
                var output = mgr.SignupUser(GetAnonymousCallContext(), new NewUserSignupInput()
                {
                     FirstName = FirstName,
                     LastName = LastName,
                     Email = UserEmail,
                     Password = Password

                });

                if (output.Success == false)
                {
                    ModelState.AddModelError("Email", output.ErrorMessage);
                   
                }
                else
                {
                    Response.Redirect("/login");
                }
            }

        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [BindProperty] [Required]
        public string Password { get; set; }


        [BindProperty]
        [Required]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        public string LastName { get; set; }

    }
}