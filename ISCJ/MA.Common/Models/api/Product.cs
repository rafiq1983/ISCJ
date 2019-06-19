using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
  public class Product
  {
    public string Id { get; set; }
    public string Description { get; set; }

    public decimal Amount { get; set; }

  }

    public class AddProductOutput
    {
        public Guid ProductId { get; set; }
    }

    public class AddProductInput
    {
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public string ProductCode { get; set; }
    }
}
