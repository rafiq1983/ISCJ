using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.User;
using MA.Common.Models.api;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class UserMgmtController : BaseController
    {
        [HttpPost()]
        public void AddUser(CreateUserInput input)
        {

        }

        [HttpGet("GetByEmail")]
        public List<MA.Common.Entities.User.User> SearchByEmail([FromQuery]string q)
        {
            return null;
        }
    }
}