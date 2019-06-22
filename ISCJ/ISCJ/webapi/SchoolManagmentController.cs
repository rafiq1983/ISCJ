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
    public class SchoolManagmentController : ControllerBase
    {
        [HttpPost("teacher")]
        public string AddTeacher(string teacherId)
        {
            return "added";
        }

    }
}