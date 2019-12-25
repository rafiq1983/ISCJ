using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Newtonsoft.Json;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class CustomerAccount
    {
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        [JsonIgnore]//otherwise, json.net fails with circular dependency error.  Need to figure out.
        public Customer Customer { get; set; }
        public ICollection<AccountAddressLink> AddressesLink { get; set; }
        public ICollection<AccountPaymentSetting> PaymentSettings { get; set; }
        public ICollection<AccountContactLink> ContactsLink { get; set; }
    }
}
