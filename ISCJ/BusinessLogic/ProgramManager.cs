using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Core;
using MA.Common.Entities.Registration;

namespace BusinessLogic
{
  public class ProgramManager
  {
    
        public Guid AddProgram(CallContext context, string programName, string programDesc)
        {
            using (var db = new Database())
            {
                var programDetail = new ProgramDetail();
                programDetail.ProgramId = Guid.NewGuid();
                programDetail.ProgramName = programName;
                programDetail.ProgramDescription = programDesc;
                programDetail.CreateUser = context.UserId;
                programDetail.CreateDate = DateTime.UtcNow;
                db.Programs.Add(programDetail);
                db.SaveChanges();
                return programDetail.ProgramId;
            }
        }

    public List<ProgramDetail> GetAllPrograms(CallContext callerContext)
    {
        List<ProgramDetail> details = new List<ProgramDetail>();
        details.Add(new ProgramDetail() {ProgramId = Guid.Empty, ProgramName = "2019"});
        //return details;
            using (var db = new Database())
            {
                return db.Programs.ToList();
            }
    }

    public ProgramDetail GetProgram(CallContext callerContext, Guid programId)
    {
            using (var db = new Database())
            {
                return db.Programs.SingleOrDefault(x => x.ProgramId == programId);
            }

        }

    public ProgramDetail GetProgramByname(CallContext callerContext, string programName)
    {
        using (var db = new Database())
        {
            return db.Programs.SingleOrDefault(x => x.ProgramName == programName);
        }

    }

  }

}

