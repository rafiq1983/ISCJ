using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    //Link entity.  Represents Contact to email relation.
    public class ContactEmail
    {
        public string ContactId { get; set; }
        public string EmailId { get; set; }
        public Contact Contact { get; set; }
        public Email Email { get; set; }
    }
}
