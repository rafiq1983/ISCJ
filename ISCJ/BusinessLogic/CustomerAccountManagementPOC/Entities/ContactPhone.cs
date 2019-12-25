using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    //Link entity.
    public class ContactPhone
    {
        public string ContactId { get; set; }
        public string PhoneId { get; set; }
        public string PhoneType { get; set; }
        public Contact Contact { get; set; }
        public Phone Phone { get; set; }
    }
}
