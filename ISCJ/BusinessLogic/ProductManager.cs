using MA.Common;
using MA.Common.Entities.Product;
using MA.Common.Models.api;
using MA.Core;
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

        

        public Product GetBillingItem(string productId)
        {
            return _BillableItems.SingleOrDefault(x => x.Id == productId);
        }


        public AddProductOutput AddProduct(CallContext callContext, AddProductInput input)
        {
            using (var db = new Database())
            {
                var existingProduct = db.BillableProducts.SingleOrDefault(x => x.TenantId == callContext.TenantId &&
                                                                       x.ProductCode == input.ProductCode);

                if (existingProduct != null)
                {
                    return new AddProductOutput()
                    {
                        FailureMessage = "Product Code already exists",
                        Success = false
                    };
                }

                BillableProduct product = new BillableProduct()
                {
                    ProductId = Guid.NewGuid(),
                    Price = input.Price,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = callContext.UserId,
                    EffectiveDate = input.EffectiveDate,
                    ExpirationDate = input.ExpirationDate,
                    IsActive = Convert.ToByte(input.IsActive),
                    Description = input.Description,
                    ProductCode = input.ProductCode,
                    TenantId = callContext.TenantId

                };

                db.BillableProducts.Add(product);
                db.SaveChanges();
                return new AddProductOutput()
                {
                    ProductId = product.ProductId,
                    Success = true
                };
            }
        }

        public BillableProduct GetProductById(CallContext callContext, Guid productId)
        {
            using (var db = new Database())
            {
                return db.BillableProducts.SingleOrDefault(x => x.ProductId == productId);
            }
        }

        public List<BillableProduct> GetAllProducts(CallContext callContext)
        {
            var output = new List<BillableProduct>();
            output.Add(new BillableProduct() {Price = 33, Description = "sunday school fee regular", ProductCode = "afafdsa"});
            output.Add(new BillableProduct() { Price = 45, Description = "sunday school fee late", ProductCode = "afafdsa2" });
            //return output;
            using (var db = new Database())
            {
                return db.BillableProducts.Where(x => x.TenantId == callContext.TenantId).ToList();
            }
        }

        public BillableProduct GetProductByCode(CallContext callContext, string productCode)
        {
            using (var db = new Database())
            {
                return db.BillableProducts.SingleOrDefault(x => x.ProductCode == productCode);
            }
        }


    }

  }

