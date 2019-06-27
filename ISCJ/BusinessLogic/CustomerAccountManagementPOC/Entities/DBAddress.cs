using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
   public class DBAddress:EFBaseEntity //Iftikhar: Calling it DB Address so it doesn't conflict with existing Address classes.  If you call it address, swagger doesn't work with out additonal configuration.
    {
        public string AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public ICollection<AccountAddressLink> AccountsLink { get; set; }
    }
}
