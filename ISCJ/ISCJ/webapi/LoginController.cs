using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes=CookieAuthenticationDefaults.AuthenticationScheme)] //this endpoint will do validation via cookie generated from login page and get a jwt token.
    public class LoginController : ControllerBase
    {
        [HttpGet("jwt")]
        public string GetJsonToken()
        {
            //U can use these in other overloads.
          //  JwtHeader header = new JwtHeader();
          //  JwtPayload payload = new JwtPayload("ISCJApp", "ISCJAppUser", null, null, DateTime.Now.AddMinutes(30));

            JwtSecurityToken token = new JwtSecurityToken("ISCJApp", "ISCJAppUser", null, null,
                DateTime.Now.AddMinutes(10), new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ISCJAppSigniningKey")), SecurityAlgorithms.HmacSha256));


            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}