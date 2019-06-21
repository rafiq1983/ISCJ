using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class Phone:EFBaseEntity
    {
        public string PhoneId { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
