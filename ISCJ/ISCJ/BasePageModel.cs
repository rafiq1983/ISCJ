using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.User;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ
{
    public class BasePageModel:PageModel
    {
        protected virtual MA.Core.CallContext GetCallContext()
        {
            var tenantIdClaim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == AppClaimTypes.TenantId);
            string tenantId = tenantIdClaim != null ? tenantIdClaim.Value : string.Empty;
            var name = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            return new CallContext(name, "", tenantId, Guid.Parse(tenantId));
        }

        public bool HasSelectedOrganization
        {
            get
            {
                var tenantIdClaim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == AppClaimTypes.TenantId);
                return tenantIdClaim != null;
            }
        }

        public int GetUserNotificationCount()
        {
            UserManager mgr = new UserManager();
            return mgr.GetUserNotificationsCount(GetCallContext());

        }

        public List<UserNotificationViewDisplay> GetUserNotification()
        {
            UserManager mgr = new UserManager();
            var notifications = mgr.GetUserNotifications(GetCallContext());

            List<UserNotificationViewDisplay> output = new List<UserNotificationViewDisplay>();

            foreach (var notification in notifications)
            {
                output.Add(new UserNotificationViewDisplay()
                {
                    
                    Message = GetMessage(notification),
                    HoursAgo = GetHoursAgo(notification),
                    RecievedDateTime = GetReceivedDateTime(notification)

                });
            }

            return output;
        }

        private string GetHoursAgo(UserNotification notification)
        {
            return (DateTime.Now - notification.CreateDate).Hours + " Ago";
        }

        private string GetReceivedDateTime(UserNotification notification)
        {
            return "Received on " + notification.CreateDate.ToLongDateString();
        }

        private string GetMessage(UserNotification notification)
        {
            if (notification.NotificationType == "AddedToOrganization")
            {
                return notification.NotificationData;
            }
            else
            {
                return "";
            }
        }
    }
}
