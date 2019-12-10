using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using ISCJ.TagHelpers;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.User;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ISCJ
{
    public class BasePageModel:PageModel
    {
        private NavigationBar _leftNavBar = null;

        public NavigationBar LeftNavigationBar
        {
            get
            {
                if (_leftNavBar == null)
                {
                    LoadLeftNavBar();
                }

                return _leftNavBar;
            }
        }

        public void LoadLeftNavBar()
        {
            _leftNavBar = new NavigationBar();
            _leftNavBar.Sections = new List<Section>();

            CallContext context = GetCallContext();


            Section meSection = null;
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Profile", "/Me/UserProfile"));
            sectionItems.Add(GetSectionItem("My Organizations", "/Me/SelectTenant"));
            sectionItems.Add(GetSectionItem("Create New Organization", "/signup/signuporganization"));
            sectionItems.Add(GetSectionItem("My Messages", "/Me/usermessages"));
            sectionItems.Add(GetSectionItem("My Alerts", "/me/useralerts"));

            meSection = new Section("Me", sectionItems);

            _leftNavBar.Sections.Add(meSection);

            if (context.TenantId.HasValue == true)
            {
                _leftNavBar.Sections.Add(new Section("setup",new List<SectionItem>()));

               
                _leftNavBar.Sections.Add(new Section("Contact Management", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Registration", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Student", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Financials", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Reporting", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Donors", new List<SectionItem>()));

                _leftNavBar.Sections.Add(new Section("Members", new List<SectionItem>()));




            }


        }

        private SectionItem GetSectionItem(string caption, string navUrl)
        {
            return new SectionItem(caption, HtmlHelpers.IsPageActive(RouteData.Values["page"].ToString(), navUrl), navUrl);
          
        }

       

        public string GetContactName(Contact c)
        {
            if (c == null)
                return "null";
            return c.FirstName + " " + c.LastName;
        }

        protected virtual MA.Core.CallContext GetCallContext()
        {
            
            var tenantIdClaim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == AppClaimTypes.TenantId);
            Guid? tenantId = null;
            var loginName = HttpContext.User.Claims.First(x => x.Type == AppClaimTypes.LoginName).Value;
            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == AppClaimTypes.UserId).Value);
            if (tenantIdClaim!=null)
                tenantId = Guid.Parse(tenantIdClaim.Value);

            return new CallContext(userId, loginName,Request.HttpContext.Connection.RemoteIpAddress.ToString(), tenantId?.ToString(), tenantId);
        }

        protected virtual MA.Core.CallContext GetAnonymousCallContext()
        {
            return new CallContext(Guid.Empty, "", Request.HttpContext.Connection.RemoteIpAddress.ToString(), "", null, "TBD");
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
            return (DateTime.Now - notification.CreateDate).Hours + "h Ago";
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
