using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
  
  public class InvoiceManager
  {
    private static List<Invoice> _Invoices = new List<Invoice>();

    public List<Invoice> GetInvoices(CallContext context)
    {
      using (Database db = new Database())
      {
                return db.Invoices.Where(x => x.TennantId == context.TenantId).ToList();
      }
    }

        public Invoice GetInvoiceById(Guid invoiceId)
        {
            using (Database db = new Database())
            {
                return db.Invoices.SingleOrDefault(x => x.InvoiceId == invoiceId);
            }
        }

        public Invoice GetInvoice(Guid invoiceId)
    {
      return _Invoices.SingleOrDefault(x => x.InvoiceId == invoiceId);
    }

    
    public CreateInvoiceOutput CreateInvoice(CreateInvoiceInput input)
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
                    FinancialAccountId = Guid.Empty,
                    IsPaid = false,
                    CreateDate = DateTime.Now,
                    CreateUser = "Iftikhar"
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
                invoice.IsPaid = input.IsPaid;
                invoice.InvoiceAmount = input.Amount;
              
                db.SaveChanges();
                return new UpdateInvoiceOutput() { Success = true };
            }
        }


    }


}
