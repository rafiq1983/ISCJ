using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.User;
using MA.Core.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class UserProfileModel : BasePageModel
    {
        public void OnGet()
        {
            UserManager mgr = new UserManager();
           User u =  mgr.GetUsersByLoginName(HttpContext.User.FindFirstValue(AppClaimTypes.LoginName));
           Email = u.UserName;

        }

        public string Email { get; set; }
        
    }
}