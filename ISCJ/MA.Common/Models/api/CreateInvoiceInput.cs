﻿using MA.Common.Entities.Invoices;
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

        public string OrderId { get; set; }

        public InvoiceOrderType OrderType { get; set; }
    }

    public class UpdateInvoiceInput
    {
        public decimal PaidAmount { get; set; }
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

    public class GetInvoiceInput
    {

    }

    public class GetInvoiceOutput
    {
        public List<Invoice> Invoices { get; set; }
    }
}