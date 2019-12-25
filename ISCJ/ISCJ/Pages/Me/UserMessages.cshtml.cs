using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class UserMessagesModel : BasePageModel
    {
        public void OnGet()
        {
            UserManager mgr = new UserManager();
            UserNotifications = mgr.GetUserNotifications(GetCallContext());
        }

        public List<UserNotification> UserNotifications { get; private set; }
    }
}