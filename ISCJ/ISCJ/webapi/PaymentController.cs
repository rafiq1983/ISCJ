﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class PaymentsController : BaseController
    {
        [HttpPost()]
        public JsonResult CreatePayment([FromBody]CreatePaymentInput input)
        {
            PaymentsManager mgr = new PaymentsManager();
            return new JsonResult(mgr.CreatePayment(GetCallContext(), input));
        }

        [HttpGet()]
        public JsonResult GetAllPayments()
        {
            PaymentsManager mgr = new PaymentsManager();
            return new JsonResult(mgr.GetAllPayments(GetCallContext()));
        }

        [HttpGet("cashpayments")]
        public JsonResult GetCashPayments()
        {
            PaymentsManager mgr = new PaymentsManager();
            return new JsonResult(mgr.GetCashPayments(GetCallContext()));
        }

        [HttpGet("checkpayments")]
        public JsonResult GetCheckPayments()
        {
            PaymentsManager mgr = new PaymentsManager();
            return new JsonResult(mgr.GetCheckPayments(GetCallContext()));
        }

       
    }
}