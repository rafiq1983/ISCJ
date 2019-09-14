using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Contacts;

namespace MA.Common.Models.api
{
    public class CreateMasjidMembershipInput
    {
       public Guid? ContactId { get; set; }
       public List<ProductSelected> BillingInstructions { get; set; }
       public Contact MemberContact { get; set; }
       public bool AddNewContact { get; set; }
       public DateTime EffectiveDate { get; set; }
       public DateTime ExpirationDate { get; set; }
    }

    public class CreateMasjidMembershipOutput
    {
        public Guid MembershipId { get; set; }
        public Guid ContactId { get; set; }
    }
}
