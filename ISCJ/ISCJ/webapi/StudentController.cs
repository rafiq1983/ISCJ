using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MA.Common.Models.api;
using MA.Core.Web;
using Microsoft.AspNetCore.Authorization;

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class StudentController : BaseController
    {
        private readonly StudentManager _studentManager;

        public StudentController(StudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        /// <summary>
        /// Gets a list of students for a given program.
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [Route("{programId}")]
        [HttpGet()]
        public ActionResult<List<Student>> GetStudentsListByProgamId(Guid programId)
        {
            var output = _studentManager.GetStudentList(GetCallContext(), programId);
            return Ok(output);
        }

       
      /*  [HttpGet()]
        public ActionResult<List<StudentBasicInfo>> GetStudentsList([FromQuery]string filterParams)
        {
            var output = _studentManager.GetStudentList(Guid.Empty);
            return Ok(output.Students);
        }
        */
        [Route("detail/{studentId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(StudentDetail), 200)]
        public ActionResult<Student> GetStudentDetail(Guid studentId)
        {
            var output = _studentManager.GetStudent(GetCallContext(),studentId);
            return Ok(output);
        }


        [Route("attendance/{ClassSessionId}/{studentId}/{attendance}")]
        [HttpGet()]
        public void MarkStudentPresence(string classSessionId, string studentId, bool attendance)
        {
            ;
        }

        //@Rafiq -- Implement other operations for a student.
    }
}