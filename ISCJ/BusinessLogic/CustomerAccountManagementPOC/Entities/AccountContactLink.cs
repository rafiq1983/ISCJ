using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class AccountContactLink
    {
        public string ContactId { get; set; }
        public string AccountId { get; set; }
        public string RolePosition { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public Contact Contact { get; set; }
    }
}
