using MA.Common.Entities.Product;
using System;
using System.Collections.Generic;
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
}
