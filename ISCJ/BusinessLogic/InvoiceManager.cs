using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
  
  public class InvoiceManager
  {

        public void PerformBilling(CallContext context, Database db, List<ProductSelected> billingInstructions, string desc, string orderId, ReferenceType referenceType)
        {
            if (billingInstructions.Count == 0)
                return;

           
                var productIds = billingInstructions.Where(y => y.IsSelected).Select(z => z.ProductCode).ToArray();

                var products = db.BillableProducts.Where(x => productIds.Contains(x.ProductCode)).ToDictionary(y => y.ProductCode);

                Invoice invoice = new Invoice();
                invoice.DueDate = DateTime.UtcNow;
                invoice.GenerationDate = DateTime.UtcNow;
                invoice.CreateUser = context.UserLoginName;
                invoice.ReferenceId = orderId;
                invoice.ReferenceType = referenceType;
                invoice.TennantId = context.TenantId.Value;
                invoice.CreateDate = DateTime.UtcNow;
                invoice.ModifiedDate = null;
                invoice.InvoiceTypeId = db.InvoiceTypes.SingleOrDefault(x => x.InvoiceTypeName == "Membership-fees")
                    ?.InvoiceTypeId;

                invoice.InvoiceItems = new List<InvoiceItem>();
                foreach (var item in billingInstructions.Where(x => x.IsSelected))
                {
                    if (products.ContainsKey(item.ProductCode))
                    {
                        invoice.InvoiceAmount += item.ProductCount * products[item.ProductCode].Price;
                        invoice.InvoiceItems.Add(new InvoiceItem()
                        {
                            Amount = products[item.ProductCode].Price * item.ProductCount,
                            Description = products[item.ProductCode].Description,
                            Quantity = products[item.ProductCode].SelectedCount,
                            CreateUser = context.UserLoginName,
                            CreateDate = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        throw new Exception("Invalid Product id " + item.ProductId);
                    }
                }

                invoice.Description = desc;

                db.Invoices.Add(invoice);
               
            
        }

        public List<Invoice> GetInvoices(CallContext context)
    {
      using (Database db = new Database())
      {
                var invoices = db.Invoices.Where(x => x.TennantId == context.TenantId).ToList();

                return invoices;

      }
    }

        public Invoice GetInvoiceById(Guid invoiceId)
        {
            using (Database db = new Database())
            {
                return db.Invoices.SingleOrDefault(x => x.InvoiceId == invoiceId);
            }
        }

        public List<InvoiceType> GetInvoiceTypes(CallContext context)
        {
            using (Database db = new Database())
            {
                return db.InvoiceTypes.Where(x => x.TenantId == context.TenantId).ToList();

            }
        }


        public AddInvoiceTypeOuptut AddInvoiceType(CallContext callContext, string invoiceTypeName, string shortDesc)
        {
            using (Database db = new Database())
            {
               var invoiceType = db.InvoiceTypes.SingleOrDefault(x =>
                    x.InvoiceTypeName == invoiceTypeName && x.TenantId == callContext.TenantId);

               if (invoiceType == null)
               {
                   invoiceType = new InvoiceType()
                   {
                       InvoiceTypeId = Guid.NewGuid(),
                       InvoiceTypeName = invoiceTypeName,
                       TenantId = callContext.TenantId.Value,
                       ShortDescription = shortDesc,
                       CreateDate = DateTime.UtcNow,
                       CreateUser = callContext.UserLoginName

                   };

                   db.InvoiceTypes.Add(invoiceType);
                   db.SaveChanges();
               }

               return new AddInvoiceTypeOuptut()
               {
                   Success = true,
                   InvoiceTypeId = invoiceType.InvoiceTypeId
               };

            }
        }

        public CreateInvoiceOutput CreateInvoice(CallContext callContext, CreateInvoiceInput input)
        {
            using (Database db = new Database())
            {
                var invoice = new Invoice()
                {
                    InvoiceId = Guid.NewGuid(),
                    InvoiceAmount = input.Amount,
                    Description = input.Description,
                    DueDate = DateTime.Now.AddMonths(1),
                    GenerationDate = DateTime.Now,
                    InvoiceTypeId = Guid.Parse(input.InvoiceTypeId),
                    FinancialAccountId = Guid.Empty,
                    ReferenceType = ReferenceType.AdHocInvoiceAttachedToContact,
                    ReferenceId = input.ReferenceId,
                    IsPaid = false,
                    TennantId = callContext.TenantId.Value,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = callContext.UserLoginName
                };

                db.Invoices.Add(invoice);

                db.SaveChanges();
                return new CreateInvoiceOutput() { InvoiceId = invoice.InvoiceId };
            }
        }

        public UpdateInvoiceOutput UpdateInvoice(UpdateInvoiceInput input)
        {
            using (Database db = new Database())
            {
                var invoice = db.Invoices.SingleOrDefault(x => x.InvoiceId == input.InvoiceId);
                if (invoice == null)
                    throw new Exception("Invalid Invoice id " + input.InvoiceId);

                invoice.IsPaid = input.IsPaid;
                invoice.TotalPaid = invoice.TotalPaid + input.PaidAmount;
              
                db.SaveChanges();
                return new UpdateInvoiceOutput() { Success = true };
            }
        }


    }


}
