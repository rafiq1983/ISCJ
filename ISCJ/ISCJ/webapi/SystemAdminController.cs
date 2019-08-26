using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MA.Core.Web.Filters;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("allusers")]
        //TODO: Some how tell swagger to take this value in header without having to specify that as method param.
        public JsonResult GetAllUsers([FromHeader(Name = "x-super-admin-proof")]string signedClaim)
        {
            using (var db = new BusinessLogic.Database())
            {
                var users = db.Users.Include(x=>x.Contact).AsNoTracking().ToList();
                
                return new JsonResult(users);
            }
        }

        [HttpGet("alltenants")]
        //TODO: Some how tell swagger to take this value in header without having to specify that as method param.
        public JsonResult GetAllTenants([FromHeader(Name = "x-super-admin-proof")]string signedClaim)
        {
            using (var db = new BusinessLogic.Database())
            {
                var users = db.Tenants.AsNoTracking().ToList();

                return new JsonResult(users);
            }
        }
    }
}