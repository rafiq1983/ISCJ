using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    public class CreateInvoiceInput
    {
        public decimal Amount { get; set; }
        public string ContactId { get; set; }

        public string Description { get; set; }
    }

    public class UpdateInvoiceInput
    {
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }

        public Guid InvoiceId { get; set; }
    }

    public class CreateInvoiceOutput
    {
        public Guid InvoiceId { get; set; }
    }

    public class UpdateInvoiceOutput
    {
        public bool Success { get; set; }
    }
}
