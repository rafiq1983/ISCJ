using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class InvoiceController : ControllerBase
    {

        [HttpGet("invoiceTypes")]
        public JsonResult GetInvoiceTypes()
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.GetInvoiceTypes(GetCallContext());
            return new JsonResult(output.Select(x=>new {Key = x.InvoiceTypeId, Value = x.InvoiceTypeName}));
        }

        [HttpPost("invoiceType")]
        public JsonResult AddInvoiceType([FromBody]string invoiceTypeName)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.AddInvoiceType(GetCallContext(), invoiceTypeName);
            return new JsonResult(output.InvoiceTypeId);

        }
        [HttpPost]
        public JsonResult CreateInvoice(CreateInvoiceInput input)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.CreateInvoice(GetCallContext(), input);
            return new JsonResult(output.InvoiceId);
        }

        [HttpPut]
        public JsonResult UpdateInvoice(UpdateInvoiceInput input)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.UpdateInvoice(input);
            return new JsonResult(output.Success);
        }

        [HttpGet("")]
        public JsonResult GetAllInvoices()
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.GetInvoices(GetCallContext());
            return new JsonResult(output);
        }


        [HttpPost("/financialaccount")]
        public JsonResult CreateFinancialAccount([FromBody]CreateFinancialAccountInput input)
        {
            FinancialAccountManager mgr = new FinancialAccountManager();

            return new JsonResult(mgr.CreateFinancialAccount(GetCallContext(),input));

        }

        [HttpGet("/financialaccount")]
        public JsonResult GetFinancialAccounts()
        {
            FinancialAccountManager mgr = new FinancialAccountManager();

            return new JsonResult(mgr.GetFinancialAccounts(GetCallContext()));

        }


        [HttpGet("{invoiceId}")]
        public JsonResult GetInvoiceDetail(Guid invoiceId)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.GetInvoiceById(invoiceId);
            if (output == null)
            {
                var js = new JsonResult(null);
                js.StatusCode = 404;
                return js;
                //return new NotFoundJsonResult();
            }
           
           return new JsonResult(output);
        }

        public class NotFoundJsonResult:JsonResult
            {
                public NotFoundJsonResult():base(null)
            {
                base.StatusCode = 404;
            }

            }

        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "DSFsfsd", "FDSDF", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }

    }
}