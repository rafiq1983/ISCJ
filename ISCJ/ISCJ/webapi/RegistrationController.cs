using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
       [HttpPost("registratonapplication")]
       public CreateRegistrationApplicationOutput CreateRegistration(CreateRegistrationApplicationInput input)
        {
            return new CreateRegistrationApplicationOutput()
            {
                ApplicationId = Guid.Empty
            };

        }
    }
}