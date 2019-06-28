using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        [HttpPost()]
        public JsonResult CreatePayment([FromBody]CreatePaymentInput input)
        {
            PaymentsManager mgr = new PaymentsManager();
            return new JsonResult(mgr.CreatePayment(GetCallContext(), input));
        }

        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "sdfasfds", "SDFSDF", Guid.Empty);
        }
    }
}