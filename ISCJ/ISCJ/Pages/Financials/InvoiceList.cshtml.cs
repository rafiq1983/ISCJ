using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.Financials
{
    public class InvoiceListModel : PageModel
    {
    readonly InvoiceManager _invoiceMgr = new InvoiceManager();
        public void OnGet()
        {
      Invoices = _invoiceMgr.GetInvoices(GetCallContext());
        }

    public List<Invoice> Invoices { get; set; } = new List<Invoice>();

        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "asfasfdds", "asdfasfsd", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }
    }

}
