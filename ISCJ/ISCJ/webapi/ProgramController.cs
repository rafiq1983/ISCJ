using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        [HttpPost]
        public string AddProgram(ProgramDetail detail)
        {
            ProgramManager mgr = new ProgramManager();
            return mgr.AddProgram(detail);
        }

        [HttpGet("")]
        public List<ProgramDetail> GetAllPrograms()
        {
            ProgramManager mgr = new ProgramManager();
            return mgr.GetAllPrograms(new CallContext("34234", "127.0.0.1", "32423434"));
        }

        [HttpGet("{programId}")]
        public ActionResult GetProgramDetail(string programId)
        {
            ProgramManager mgr = new ProgramManager();
            var output = mgr.GetProgram(new CallContext("34234", "127.0.0.1", "32423434"), programId);
            if (output == null)
                return NotFound();
            else
            {
                return new JsonResult(output);
            }
        }
    }
}