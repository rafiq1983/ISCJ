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

        public void OnPostCancel()
        {
            string s = "";
        }

        public void OnPostReset()
        {
           
        }

        public void OnPostSave()
        {
            string s = "";
        }


        public string Email { get; set; }
        public string PasswordResetQuestion { get; set; }
        public string PasswordResetQuestionAnswer { get; set; }

    }
}