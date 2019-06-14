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
    }
}
