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
                programDetail.TenantId = context.TenantId;
                db.Programs.Add(programDetail);
                db.SaveChanges();
                return programDetail.ProgramId;
            }
        }

    public List<ProgramDetail> GetAllPrograms(CallContext callerContext)
    {
      
            using (var db = new Database())
            {
                return db.Programs.Where(x => x.TenantId == callerContext.TenantId).ToList();
            }
    }

    public ProgramDetail GetProgram(CallContext callerContext, Guid programId)
    {
            using (var db = new Database())
            {
                return db.Programs.SingleOrDefault(x => x.ProgramId == programId && x.TenantId==callerContext.TenantId);
            }

        }

    public ProgramDetail GetProgramByName(CallContext callerContext, string programName)
    {
        using (var db = new Database())
        {
            return db.Programs.SingleOrDefault(x => x.ProgramName == programName && x.TenantId == callerContext.TenantId);
        }

    }

  }

}

