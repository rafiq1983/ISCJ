using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MA.Common.Entities.Registration;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ProgramController : BaseController
    {
        [HttpPost]
        public Guid AddProgram(ProgramDetail detail)
        {
            ProgramManager mgr = new ProgramManager();
            return mgr.AddProgram(GetCallContext(), detail.ProgramName, detail.ProgramDescription);
        }

        [HttpGet("")]
        public List<ProgramDetail> GetAllPrograms()
        {
            ProgramManager mgr = new ProgramManager();
            return mgr.GetAllPrograms(GetCallContext());
        }

        [HttpGet("{programId}")]
        public ActionResult GetProgramDetail(Guid programId)
        {
            ProgramManager mgr = new ProgramManager();
            var output = mgr.GetProgram(GetCallContext(), programId);
            if (output == null)
                return NotFound();
            else
            {
                return new JsonResult(output);
            }
        }
    }
}