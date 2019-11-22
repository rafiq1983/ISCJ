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
            var tenantId = HttpContext.User.Claims.Single(x => x.Type == AppClaimTypes.TenantId).Value;
            var name = HttpContext.User.Claims.Single(x => x.Type ==ClaimTypes.Email).Value;
            return new CallContext(name, "", tenantId, Guid.Parse(tenantId));
        }
    }


    public class AppClaimTypes
    {
        public const string TenantName = "TenantName";
        public const string TenantId = "TenantId";
    }
}
