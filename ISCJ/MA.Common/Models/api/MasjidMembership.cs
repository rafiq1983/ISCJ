using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    public class CreateMasjidMembershipInput
    {
       public bool AddNewContact { get; set; }
       public MA.Common.Entities.Contacts.Contact Contact{ get; set; } //iftikhar: should really take model contact.
        public Guid? ContactId { get; set; }
        
    }

    public class CreateMasjidMembershipOutput
    {
        public Guid MembershipId { get; set; }
        public Guid ContactId { get; set; }
    }
}
