using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core.Web.Filters
{
    public class SuperAdminAccessFilter : Attribute,IAuthorizationFilter
    {
        //use this public key to confirm that data was signed by it's private key.
        string publicKey = "AAAAB3NzaC1yc2EAAAADAQABAAAAgQCQ4k/4x3SjaOn0QOjZ83dPAo/T+ztPWjUmLY8EUlrXxcPGMB0lL2Km8XudASo1k8HvkMyKBSRsV25+LT5ZE4ETjV/nMZ/sJ3Pq/5uNSUZGjHVtEsmA4Oa2iDI0Ko65rMhd+lM2Uyaojxx/gYOuUjBYeSmMROnQsGo4AnckPliabw==";

        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
           
            string superUserProofValue = context.HttpContext.Request.Headers["x-super-admin-proof"].ToString();

            if(Verify(superUserProofValue))
            {
                ;
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool Verify(string proofValue)
        {
           
            //temp.
            return proofValue == "testing123";
        }
    }
}
