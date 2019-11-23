using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MA.Common.Entities.Contacts;

namespace MA.Common.Entities.Product
{   
    public class BillableProduct
    {
        public Guid ProductId { get; set; }


        public decimal Price { get; set; }

        public Guid TenantId { get; set; }

        public string Description { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public byte IsActive { get; set; }

        public string ProductCode { get; set; }

        [NotMapped] public bool IsSelected { get; set; }
        [NotMapped] public int SelectedCount { get; set; }
    }

}
