using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic;
using ISCJ.Pages.Admin;
using MA.Common.Entities.Tenants;
using MA.Core.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace ISCJ.Pages
{
    public class SelectTenantModel : BasePageModel
    {
        [BindProperty]
        public List<Tenant> Tenants { get; set; }

        public SelectTenantModel()
        {
           
        }

        public void OnGet()
        {
            UserManager mgr = new UserManager();
            ClaimsPrincipal user = Request.HttpContext.User;
            var userId = user.Claims.Single(x=>x.Type == AppClaimTypes.UserId).Value;
            Tenants = mgr.GetUserTenants(Guid.Parse(userId)).Select(x => x.Tenant).ToList();

        }

        public void OnPost(int btnLoginToTenant)
        {
            if (ModelState.IsValid)
            {
                var tenant = Tenants[btnLoginToTenant];
                //TODO:  The tenant is not switching.  See if latest information is written to the queue.
                var principal= Request.HttpContext.User;

                var claim = (principal.Identity as ClaimsIdentity).FindFirst(AppClaimTypes.TenantId);

                if (claim != null)
                {
                    (principal.Identity as ClaimsIdentity).RemoveClaim(claim); //remove claims because queries downstream expects a single value.

                }

                claim = (principal.Identity as ClaimsIdentity).FindFirst(AppClaimTypes.TenantName);

                if(claim!=null)
                 (principal.Identity as ClaimsIdentity).RemoveClaim(claim);

                //using bad claim types here. TODO: Fix it.  Tenant info can be stored as separate cookie.
                (principal.Identity as ClaimsIdentity).AddClaim(new Claim(AppClaimTypes.TenantId, tenant.TenantId.ToString()));
                (principal.Identity as ClaimsIdentity).AddClaim(new Claim(AppClaimTypes.TenantName, tenant.OrganizationName));

                var task = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });
                task.Wait();
                Response.Redirect("main");

            }

        }


    }
}