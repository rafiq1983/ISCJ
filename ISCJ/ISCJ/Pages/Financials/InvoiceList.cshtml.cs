using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.Financials
{
    public class InvoiceListModel : PageModel
    {
    readonly InvoiceManager _invoiceMgr = new InvoiceManager();
        public void OnGet()
        {
      Invoices = _invoiceMgr.GetInvoices();
        }

    public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
