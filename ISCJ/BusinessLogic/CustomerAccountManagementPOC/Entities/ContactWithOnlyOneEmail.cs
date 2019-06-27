using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    //This is the demo of showing one to one relation even if the db allows 1 to many.
    public class ContactWithOnlyOneEmail
    {
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmailSingle Email { get; set; }
    }

    public class EmailSingle
    {
        public string ContactId { get; set; }
        public string EmailId { get; set; }
        public Email Email { get; set; }
        public ContactWithOnlyOneEmail ContactWithOnlyOneEmail { get; set; }

    }
    
}
