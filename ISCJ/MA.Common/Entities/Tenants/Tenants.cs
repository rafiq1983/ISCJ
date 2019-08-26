using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Tenants
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string TenantDescription { get; set; }
        public Guid? LogoId { get; set; }
        public string DomainName { get; set; }

        public string RowVersion { get; set; }
    }
}
