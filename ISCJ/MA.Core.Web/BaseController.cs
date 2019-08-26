using Microsoft.AspNetCore.Mvc;
using System;

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
            return new CallContext("Iftikhar", "", "", Guid.Empty);
        }
    }
}
