using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpPost]
        public JsonResult CreateInvoice(CreateInvoiceInput input)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.CreateInvoice(input);
            return new JsonResult(output.InvoiceId);
        }

        [HttpPut]
        public JsonResult CreateInvoice(UpdateInvoiceInput input)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.UpdateInvoice(input);
            return new JsonResult(output.Success);
        }

        [HttpGet("")]
        public JsonResult GetAllInvoices()
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.GetInvoices();
            return new JsonResult(output);
        }

        [HttpGet("{invoiceId}")]
        public JsonResult GetInvoiceDetail(Guid invoiceId)
        {
            InvoiceManager mgr = new InvoiceManager();
            var output = mgr.GetInvoiceById(invoiceId);
            if (output == null)
            {
                return new NotFoundJsonResult();
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

    }
}