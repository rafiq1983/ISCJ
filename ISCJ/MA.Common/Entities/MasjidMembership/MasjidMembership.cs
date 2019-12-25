using MA.Common.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.MasjidMembership
{
    public class MasjidMembership:BaseEntity
    {
        public Guid MembershipId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Contact Contact { get; set; }
        public Guid ContactId { get; set; }

        public Guid TenantId { get; set; }
    }

}
