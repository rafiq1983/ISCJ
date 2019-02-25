using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.Admin
{
    public class LoginModel : PageModel
    {

       
    [BindProperty]
        public LoginData loginData { get; set; }
        public void OnGet()
        {
          
        }

        public async Task OnPostAsync()
        {

            var isValid = false;
            if (ModelState.IsValid)
            {
                if(loginData.Username =="iscjapp" && loginData.Password =="password")
                {

                    isValid = true;
                }
               
                if (!isValid)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return;
                       
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginData.Username));
                identity.AddClaim(new Claim(ClaimTypes.Name, loginData.Username));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = loginData.RememberMe });
               RedirectToPage("ContactManagement/Contacts");
        
      }
            else
            {
                ModelState.AddModelError("", "username or password is blank");
                return ;
            }
        }



    }

    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

}
