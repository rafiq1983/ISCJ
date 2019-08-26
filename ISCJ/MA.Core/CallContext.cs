using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core
{
   public class CallContext
    {
        public CallContext(string userId, string callerIP, string loggedInAccountId, Guid tenantId)
        {
            UserId = userId;
            CallerIp = callerIP;
            LoggedInAccountId = loggedInAccountId;
            TenantId = tenantId;
        }
        public string UserId { get; private set; }
        public string CallerIp { get;  private set; }
        public string LoggedInAccountId { get; private set; }

        public Guid TenantId { get; private set; }
    }
}
