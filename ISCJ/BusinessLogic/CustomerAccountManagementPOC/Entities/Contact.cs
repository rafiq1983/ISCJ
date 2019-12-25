using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class Contact:EFBaseEntity
    {
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ContactPhone> Phones { get; set; }
        public ICollection<ContactEmail> Emails { get; set; }
    }
}
