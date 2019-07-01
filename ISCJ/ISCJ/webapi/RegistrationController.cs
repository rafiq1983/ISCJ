using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class RegistrationController : ControllerBase
    {
       [HttpPost("registratonapplication")]
       public CreateRegistrationApplicationOutput CreateRegistration(CreateRegistrationApplicationInput input)
        {
            RegistrationManager mgr = new RegistrationManager();
            return mgr.CreateRegistration(GetCallerContext(), input);
        }

        private CallContext GetCallerContext()
        {
            return new CallContext("Iftikhar", "sdfsdf", "DSFSDF", Guid.Empty);
        }
    }
}