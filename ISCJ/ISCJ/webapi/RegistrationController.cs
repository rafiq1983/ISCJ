using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using MA.Common.Models.api;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class RegistrationController : BaseController
    {
       [HttpPost("registratonapplication")]
       public CreateRegistrationApplicationOutput CreateRegistration(CreateRegistrationApplicationInput input)
        {
            RegistrationManager mgr = new RegistrationManager();
            return mgr.CreateRegistration(GetCallContext(), input);
        }

       [HttpPost("registratonapplication/registration")]
       public AddRegistrationOutput CreateRegistration(AddRegistrationInput input)
       {
           RegistrationManager mgr = new RegistrationManager();
           return mgr.AddEnrollmentToRegistrationApplication(GetCallContext(), input);
       }

       [HttpGet("registratonapplication")]
       public List<RegistrationApplication> GetAllRegistrations()
       {
           RegistrationManager mgr = new RegistrationManager();
           return mgr.GetAllApplications(GetCallContext(), Guid.Empty);
       }


     
    }
}