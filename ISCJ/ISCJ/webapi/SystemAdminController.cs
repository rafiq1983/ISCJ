using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MA.Core.Web.Filters;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [SuperAdminAccessFilter]
    public class SystemAdminController : ControllerBase
    {
       [HttpPost()]
        public string CreateNewTennant(string tennantDescription, [FromHeader(Name ="x-super-admin-proof")]string signedClaim)
        {
            return "Created";
        }

        [HttpPost("newuser")]
        public JsonResult CreateTennantUser(string TennantId, string userName, string password)
        {
            return new JsonResult(userName);
        }

        [HttpGet()]
        //TODO: Some how tell swagger to take this value in header without having to specify that as method param.
        public JsonResult GetAllUsers([FromHeader(Name = "x-super-admin-proof")]string signedClaim)
        {
            using (var db = new BusinessLogic.Database())
            {
                return new JsonResult(db.Users.ToList());
            }
        }
    }
}