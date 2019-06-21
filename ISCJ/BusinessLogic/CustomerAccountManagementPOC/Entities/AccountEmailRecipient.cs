using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class AccountEmailRecipient
    {
        public string AccountEmailRecipientId{ get; set; }
        public string AccountId { get; set; }
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public bool SendAutoReceiptIndicator { get; set; }
    }
}
