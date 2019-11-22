using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ
{
    public class BasePageModel:PageModel
    {
        protected virtual MA.Core.CallContext GetCallContext()
        {
            var tenantId = HttpContext.User.Claims.Single(x => x.Type == AppClaimTypes.TenantId).Value;
            var name = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
            return new CallContext(name, "", tenantId, Guid.Parse(tenantId));
        }
    }
}
