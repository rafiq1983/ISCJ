using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class UserProfileModel : BasePageModel
    {
        public void OnGet()
        {
            UserManager mgr = new UserManager();
           User u =  mgr.GetUsersByUserName(HttpContext.User.Identity.Name);
           Email = u.UserName;

        }

        public string Email { get; set; }
        
    }
}