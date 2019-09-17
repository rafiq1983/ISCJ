using System;
using System.Collections.Generic;
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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]
    public class EnrollmentController : ControllerBase
    {

        [HttpGet("Enrollments/parent/{parentid}")]
        [ProducesResponseType(typeof(List<Enrollment>),200)]
        public List<Enrollment> GetEnrollmentsByParentID(string parentid)
        {
            RegistrationManager mgr = new RegistrationManager();
            return mgr.GetEnrollmentsByParentId(Guid.Parse(parentid));
        }
         
    }
}