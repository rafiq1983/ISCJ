using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Tenants
{
    public class Tenant:BaseEntity
    {
        public Guid TenantId { get; set; }
        public string OrganizationName { get; set; }
        public Guid? LogoId { get; set; }
        public string RowVersion { get; set; }
        public bool IsVerified { get; set; }
        public Guid OwnerId { get; set; }
        public string DisplayTimeZone { get; set; }
    }

   

}
