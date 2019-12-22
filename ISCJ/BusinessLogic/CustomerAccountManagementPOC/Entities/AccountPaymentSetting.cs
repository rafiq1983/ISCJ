using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
   public class AccountPaymentSetting:EFBaseEntity
    {
        public string PaymentSettingId { get; set; }
        public string AccountId { get; set; }
        public string NameOnCard { get; set; }
        public string CardToken { get; set; }
        public string TokenSource { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear {get; set; }
        public string Last4Digits { get; set; }
        public bool IsDefault { get; set; }
        public string BillingAddressId { get; set; }
        public DBAddress BillingAddress { get; set; }
        public CustomerAccount Account { get; set; }
    }
}
