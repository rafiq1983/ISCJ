using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{

    public class Customer:EFBaseEntity
    {
        public string CustomerId { get; set; }
        public Guid SupplierId { get; set; }
        public bool IsDeleted { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public ICollection<CustomerAccount> Accounts { get; set; }
    }
}
