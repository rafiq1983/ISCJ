using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common.Models.api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMgmtController : ControllerBase
    {
        [HttpPost()]
        public void AddUser(CreateUserInput input)
        {

        }
    }
}