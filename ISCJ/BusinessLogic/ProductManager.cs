using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
  public class ProductManager
  {
    private static List<Product> _BillableItems = new List<Product>();

    static ProductManager()
    {
      var item = new Product();

      item.Id = "443cc8fe-29c3-4a5e-b42d-1d00ad26977f";
      item.Description = "Yearly Membership Fee.";
      item.Amount = 365;

      _BillableItems.Add(item);

      item = new Product();

      item.Id = "4aab2eb7-ae52-4101-bd6d-b9142829a50d";
      item.Description = "Child School Fee.";
      item.Amount = 20;

      _BillableItems.Add(item);

      item = new Product();
      item.Id = Guid.NewGuid().ToString();
      item.Description = "Child School Fee delayed registration.";
      item.Amount = 25;

      _BillableItems.Add(item);

    }

    public List<Product> GetBillingItems()
    {
      return _BillableItems;
    }

    public Product GetBillingItem(string productId)
    {
      return _BillableItems.SingleOrDefault(x => x.Id == productId);
    }
  }
}
