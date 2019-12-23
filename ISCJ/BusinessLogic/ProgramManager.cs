using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MA.Core;
using MA.Common.Entities.Registration;
using Microsoft.EntityFrameworkCore;

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
                programDetail.CreateUser = context.UserLoginName;
                programDetail.CreateDate = DateTime.UtcNow;
                programDetail.TenantId = context.TenantId.Value;
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
                return db.Programs.SingleOrDefault(
                    x => x.ProgramId == programId && x.TenantId == callerContext.TenantId);
            }

        }

        public ProgramDetail GetProgramByName(CallContext callerContext, string programName)
        {
            using (var db = new Database())
            {
                return db.Programs.SingleOrDefault(x =>
                    x.ProgramName == programName && x.TenantId == callerContext.TenantId);
            }

        }


        public List<Metric> GetMetrics(CallContext context)
        {
            using (var db = new Database())
            {
                return db.Metrics.Where(x => x.TenantId == context.TenantId).ToList();
            }
        }

        public List<MetricAssociation> GetAssociatedMetrics(CallContext context, Guid entityId)
        {
            using (var db = new Database())
            {
                return db.MetricAssociations.Where(x => x.TenantId == context.TenantId && x.EntityId == entityId).ToList();
            }
        }

        public List<MetricAssociation> GetEntityMetricsAssociations(CallContext context, Guid entityId)
        {
            using (var db = new Database())
            {
                return db.MetricAssociations.Where(x => x.TenantId == context.TenantId && x.EntityId == entityId).ToList();
            }
        }

        public string GetEntityName(CallContext context, Guid entityId, string entityType)
        {
            using (var db = new Database())
            {
                if (entityType.ToLower() == "subject")
                {
                   var subject = db.Subjects.Where(x=>x.TenantId == context.TenantId && x.SubjectId == entityId).SingleOrDefault();
                   return subject==null?"Unknown Entity":subject.SubjectName;
                }
                else
                {
                    return "Unknown Entity";
                }
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
                m.TenantId = context.TenantId.Value;
                m.MetricId = Guid.NewGuid();
                m.MetricDescription = input.MetricDescription;
                m.MetricName = input.MetricName;
                m.MetricValueDefinition = Newtonsoft.Json.JsonConvert.SerializeObject(input.MetricValueDefinition);
                m.CreateUser = context.UserLoginName;
                m.CreateDate = DateTime.UtcNow;

                db.Metrics.Add(m);
                db.SaveChanges();

                return new AddMetricOutput()
                {
                    MetricId = m.MetricId,
                    Success = true
                };

            }
        }

        public AttachMetricOutput AssociateMetrics(CallContext context, SetMetricsToEntityInput input)
        {
            using (var db = new Database())
            {
                var existingAssociations = db.MetricAssociations.Where(x => x.TenantId == context.TenantId &&
                                                                            x.EntityId == input.EntityId).ToList();

                foreach (var item in existingAssociations)
                {
                    db.Remove(item);
                }

                foreach (var item in input.MetricIds)
                {

                    var metricAssociation =
                        BuildMetricAssociation(context, item, input.EntityId, input.EntityType);


                    db.MetricAssociations.Add(metricAssociation);
                }

                db.SaveChanges();

                return new AttachMetricOutput()
                {
                    Success = true
                };
            }
        }


        public AttachMetricOutput AddMetricAssociation(CallContext context, AttachMetricInput input)
        {
           
            using (var db = new Database())
            {
                
                var existingMetric =
                    db.MetricAssociations.SingleOrDefault(x =>
                        x.EntityId == input.EntityId && x.MetricId == input.MetricId);

                if (existingMetric != null)
                {
                    return new AttachMetricOutput()
                    {
                        ErrorMessage = "Combination already exists",
                        Success = false

                    };
                }

                var metricAssociation =
                    BuildMetricAssociation(context, input.MetricId, input.EntityId, input.EntityType);

                db.MetricAssociations.Add(metricAssociation);
                db.SaveChanges();
                
                return new AttachMetricOutput()
                {
                    Success = true
                };
            }

        }

        private MetricAssociation BuildMetricAssociation(CallContext context, Guid metricId, Guid entityId, string entityType)
        {
            MetricAssociation m = new MetricAssociation();
            m.TenantId = context.TenantId.Value;
            m.MetricId = metricId;
            m.EntityType = entityType;
            m.EntityId = entityId;

            m.CreateUser = context.UserLoginName;
            m.CreateDate = DateTime.UtcNow;
            return m;
            }
            }

  }



