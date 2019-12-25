using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class Email : EFBaseEntity
    {
        public string EmailId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
