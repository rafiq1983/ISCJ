using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        [HttpGet()]
        public List<string> GetPayments()
        {
            return new List<string>() { "1", "2" };
        }
    }
}