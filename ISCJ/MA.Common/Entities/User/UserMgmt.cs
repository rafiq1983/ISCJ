using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.User
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsAccountLocked { get; set; }
        public bool RequirePasswordChangeAtLogin { get; set; }

        //TODO: iftikhar; If contact id is commented out and contact object is present, it looks for 
        //column ContactGuid. So need to understand what is going on there
       // public Guid ContactId { get; set; }
        public Contacts.Contact Contact { get; set; }

        public string LastLoginIP{ get; set; }
        public DateTime LastLoginDate { get; set; }
        public ICollection<UserLoginHistory> LoginHistory { get; set; }

        public ICollection<UserRoleLink> UserRoles { get; set; }
    }

    public class UserLoginHistory
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginDate { get; set; }
        public string DeviceType { get; set; }
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

}
