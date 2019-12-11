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
                
                _leftNavBar.Sections.Add(GetSetupSection());
                _leftNavBar.Sections.Add(GetContactSection());
                _leftNavBar.Sections.Add(GetRegistrationSection());

                _leftNavBar.Sections.Add(GetStudentSection());
                _leftNavBar.Sections.Add(GetFinancialsSection());

                _leftNavBar.Sections.Add(GetReportingSection());

                _leftNavBar.Sections.Add(GetDonorsSection());

                _leftNavBar.Sections.Add(GetMembersSection());
                
            }


        }

        private Section GetSetupSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Manage Programs", "/setup/vieweditprogram"));
            sectionItems.Add(GetSectionItem("Manage Instructors", "/setup/AddInstructor"));
            sectionItems.Add(GetSectionItem("Manage Subjects", "/setup/AddSubject"));
            sectionItems.Add(GetSectionItem("Manage Rooms", "/setup/AddRoom"));
            sectionItems.Add(GetSectionItem("Manage Products", "/setup/ManageProducts"));
            sectionItems.Add(GetSectionItem("Add User", "/setup/AddUser"));
            sectionItems.Add(GetSectionItem("Teacher Subject Assignment", "/setup/TeacherSubjectAssignment"));
            sectionItems.Add(GetSectionItem("Course Assignment Rules", "/setup/SubjectAssignmentRules"));
            sectionItems.Add(GetSectionItem("Create Classes", "/setup/manageclasses"));
            sectionItems.Add(GetSectionItem("View Classes", "/setup/ViewClasses"));


            Section section = new Section("Setup", sectionItems);

            return section;
        }

        private Section GetContactSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Contacts", "/ContactManagement/Contacts"));
            sectionItems.Add(GetSectionItem("Add Contacts", "/ContactManagement/ContactViewEdit"));
            sectionItems.Add(GetSectionItem("Communication", "/ContactManagement/Communications"));
         

            Section section = new Section("Contact Management", sectionItems);

            return section;
        }

        private Section GetStudentSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Dashboard", "/StudentManagement/StudentDashboard"));
            sectionItems.Add(GetSectionItem("Attendance", "/StudentManagement/Attendance"));

            Section section = new Section("Student", sectionItems);

            return section;
        }

        private Section GetFinancialsSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Invoices", "/Financials/InvoiceList"));
            sectionItems.Add(GetSectionItem("Add Invoices", "/Financials/AddInvoice"));

            sectionItems.Add(GetSectionItem("Payments", "/Financials/Payments"));

            sectionItems.Add(GetSectionItem("Add Payments", "/Financials/AddPayment"));

            Section section = new Section("Financials", sectionItems);

            return section;
        }


        private Section GetRegistrationSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("New Registration Application", "/StudentManagement/RegisterStudent2"));
            sectionItems.Add(GetSectionItem("View Registration Applications", "/StudentManagement/RegistrationApplications"));
            sectionItems.Add(GetSectionItem("Enrollments", "/StudentManagement/Enrollments"));
            
            Section section = new Section("Registration", sectionItems);

            return section;
        }

        private Section GetReportingSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
             Section section = new Section("Reporting", sectionItems);

            return section;
        }

        private Section GetDonorsSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            Section section = new Section("Donors", sectionItems);

            return section;
        }

        private Section GetMembersSection()
        {
            List<SectionItem> sectionItems = new List<SectionItem>();
            sectionItems.Add(GetSectionItem("Add a member", "/Masjid/AddMasjidMemberShip"));
            sectionItems.Add(GetSectionItem("Members", "/Masjid/ViewMembers"));
            sectionItems.Add(GetSectionItem("Renew Membership", "/Masjid/RenewMembership"));

            Section section = new Section("Members", sectionItems);

            return section;
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
