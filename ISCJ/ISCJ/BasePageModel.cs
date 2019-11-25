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
    }
}
