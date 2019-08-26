using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class AccountAddress
    {
        public string AddressId { get; set; }
        public string AccountId { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public DBAddress DBAddress { get; set; }

    }
}
