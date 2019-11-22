using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Models.api;
using MA.Core;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class MembershipController : BaseController
    {
        [HttpPost()]
        public JsonResult CreateMasjidMembership(CreateMasjidMembershipInput input)
        {
            MasjidMembershipManager mgr = new MasjidMembershipManager();
            var output = mgr.CreateMasjidMembership(GetCallContext(), input);
            return new JsonResult(new { contactId = output.ContactId, membershipId = output.MembershipId });
        }


        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "234234", "askfj", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }
        [HttpGet()]
        public JsonResult GetAllMembers()
        {
            MasjidMembershipManager mgr = new MasjidMembershipManager();
            return new JsonResult(mgr.GetAllMembers());
        }

        [HttpGet("membershipbycontact/{contactId}")]
        public JsonResult GetMemberShip(Guid contactId)
        {
            MasjidMembershipManager mgr = new MasjidMembershipManager();
            return new JsonResult(mgr.GetMembershipByContactId(contactId));
        }
    }
}