using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Tenants;

namespace MA.Common.Entities.User
{
    public class User:BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsAccountLocked { get; set; }
        public bool RequirePasswordChangeAtLogin { get; set; }

        //TODO: iftikhar; If contact id is commented out and contact object is present, it looks for 
        //column ContactGuid. So need to understand what is going on there.
        //Resolution:  That's just the nature of the EF.  When it sees contact entity which has a primary key, it tries to retrieve that column from this table.
        public Guid ContactId { get; set; }
        public AuthenticationSource AuthenticationSource { get; set; }
        public Contacts.Contact Contact { get; set; }

        public string LastLoginIP{ get; set; }
        public DateTime? LastLoginDate { get; set; }
        //public ICollection<UserLoginHistory> LoginHistory { get; set; }
        public ICollection<UserRoleLink> UserRoles { get; set; }
        public List<UserTenant> UserTenants { get; set; }
    }

    public class UserLoginHistory
    {
        public Guid SessionId { get; set; }
        public string UserId { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginDate { get; set; }
        public string DeviceType { get; set; }
        public Guid? TenantId { get; set; }
    }

    public class UserRoleLink
    {
        public Guid UserId { get; set; }
        public Guid RoleCd { get; set; }

        public AppRole AppRole { get; set; }
    }

    public class AppRole
    {
        public string RoleCd { get; set; }
        public string RoleDescription { get; set; }
    }

    public class UserTenant:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string RoleCd { get; set; }
        public Tenant Tenant { get; set; }
        public User User { get; set; }
        
    }

    public class UserNotification : BaseEntity
    {
        public Guid NotificationId { get; set; }
        public string NotificationType { get; set; }
        public string NotificationData { get; set; }
    }

}
