﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class SchoolManagmentController : BaseController
    {
        [HttpPost("teacher")]
        public string AddTeacher(string teacherId)
        {
            return "added";
        }

    }
}