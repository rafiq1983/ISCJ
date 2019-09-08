using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
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

       [HttpPost("registratonapplication/registration")]
       public AddRegistrationOutput CreateRegistration(AddRegistrationInput input)
       {
           RegistrationManager mgr = new RegistrationManager();
           return mgr.AddRegistrationToRegistrationApplication(GetCallerContext(), input);
       }

       [HttpGet("registratonapplication")]
       public List<RegistrationApplication> GetAllRegistrations()
       {
           RegistrationManager mgr = new RegistrationManager();
           return mgr.GetAllApplications(GetCallerContext(), Guid.Empty);
       }


        private CallContext GetCallerContext()
        {
            return new CallContext("Iftikhar", "sdfsdf", "DSFSDF", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }
    }
}