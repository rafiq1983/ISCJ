using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace MA.Core.Web
{
    public class BaseController : Controller
    {
   
        protected virtual string GetUserId()
        {
            return "0";
        }

        protected virtual MA.Core.CallContext GetCallContext()
        {
            var tenantIdClaim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == AppClaimTypes.TenantId);
            Guid? tenantId = null;
            var loginName = HttpContext.User.Claims.First(x => x.Type == AppClaimTypes.LoginName).Value;
            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (!string.IsNullOrEmpty(tenantIdClaim.Value))
                tenantId = Guid.Parse(tenantIdClaim.Value);

            return new CallContext(userId, loginName, "TBD", tenantId?.ToString(), tenantId);
        }
    }


    public class AppClaimTypes
    {
        public const string TenantName = "TenantName";
        public const string TenantId = "TenantId";
        public const string UserId = "UserId";
        public const string LoginName = "LoginName";
    }
}
