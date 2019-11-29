using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core
{
   public class CallContext
    {
        public CallContext(Guid userId, string loginName, string callerIP, string loggedInAccountId, Guid? tenantId, string deviceType="")
        {
            UserId = userId;
            CallerIp = callerIP;
            LoggedInAccountId = loggedInAccountId;
            TenantId = tenantId;
            DeviceType = deviceType;
            UserLoginName = UserLoginName;
        }
        public Guid UserId { get; private set; }
        public string UserLoginName { get; set; }
        public string CallerIp { get;  private set; }
        public string LoggedInAccountId { get; private set; }
        public string DeviceType { get; private set; }
        public Guid? TenantId { get; private set; }
    }
}
