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


    public List<Metric> GetMetrics(CallContext context)
    {
        using (var db = new Database())
        {
            return db.Metrics.Where(x => x.TenantId == context.TenantId).ToList();
        }
    }

    public AddMetricOutput AddMetric(CallContext context, AddMetricInput input)
    {
        using (var db = new Database())
        {
            var existingMetric =
                db.Metrics.SingleOrDefault(x => x.TenantId == context.TenantId && x.MetricName == input.MetricName);

            if (existingMetric != null)
            {
                return new AddMetricOutput()
                {
                    ErrorMessage = "Metric name already exists",
                    Success = false

                };
            }

            Metric m = new Metric();
            m.TenantId = context.TenantId;
            m.MetricId = Guid.NewGuid();
            m.MetricDescription = input.MetricDescription;
            m.MetricName = input.MetricName;
            m.MetricValueDefinition = Newtonsoft.Json.JsonConvert.SerializeObject(input.MetricValueDefinition);
            m.CreateUser = context.UserId;
            m.CreateDate = DateTime.UtcNow;

            db.Metrics.Add(m);

            return new AddMetricOutput()
            {
                MetricId = m.MetricId,
                Success = true
            };

        }
        
    }

  }

}

