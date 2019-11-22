using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ
{
    public class LogoutModel : BasePageModel
    {
        public async void OnGet()
        {
            await HttpContext.SignOutAsync();
            RedirectToPage("login");
        }
    }
}