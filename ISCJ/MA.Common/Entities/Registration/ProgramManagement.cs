using MA.Common.Entities.Product;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MA.Common.Entities.Registration
{
  public class ProgramDetail:BaseEntity
  {
    public Guid ProgramId { get; set; }
    public string ProgramName { get; set; }
        public Guid TenantId { get; set; }
    public string ProgramDescription { get; set; }
  }

  public class Metric : BaseEntity
  {
      public Guid MetricId { get; set; }
      public Guid TenantId { get; set; }
      public string MetricName { get; set; }
      public string MetricDescription { get; set; }
      public string MetricValueDefinition { get; set; }
      [IgnoreDataMember()]
      public MetricValueTypeDefinition MetricValueDefinitionObject { get; set; }
      
  }
}
