using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Swashbuckle.AspNetCore.Swagger;

namespace ISCJ.Pages.Financials
{
    public class InvoiceListModel : PageModel
    {
        readonly InvoiceManager _invoiceMgr = new InvoiceManager();
        private readonly ContactManager _contactManager = new ContactManager();
        private readonly RegistrationManager _registrationManager = new RegistrationManager();

        public DataModel PageData = new DataModel();
        public void OnGet()
        {
            BuildPageData();

        }

        private void BuildPageData()
        {
            Invoices = _invoiceMgr.GetInvoices(GetCallContext());

            var contacts = _contactManager.GetAllContacts();

            var registrationApplications = _registrationManager.GetAllApplications(GetCallContext());

            PageData.RowData = new List<RowData>();

            foreach (Invoice v in Invoices)
            {
                RowData rowData = new RowData();
                rowData.Invoice = v;
                rowData.ResponsiblePartyName = "";

                var registrationApplication = registrationApplications.SingleOrDefault(x => x.ApplicationId.ToString() == v.OrderId);

                if (registrationApplication != null)
                {
                    rowData.FatherContact = _contactManager.GetContact(registrationApplication.FatherContactId);
                    rowData.MotherContact = _contactManager.GetContact(registrationApplication.MotherContactId);

                    if (rowData.FatherContact != null)
                    {
                        rowData.ResponsiblePartyName =
                            rowData.FatherContact.FirstName + " " + rowData.FatherContact.LastName;
                    }

                    if (rowData.MotherContact != null)
                    {
                        rowData.ResponsiblePartyName += "/" +
                            rowData.MotherContact.FirstName + " " + rowData.MotherContact.LastName;
                    }

                    rowData.ResponsiblePartyName = rowData.ResponsiblePartyName;
                }

                PageData.RowData.Add(rowData);
            }

            foreach (var rowData in PageData.RowData)
            {
                PageData.TotalInvoiceAmount += rowData.Invoice.InvoiceAmount;
                PageData.TotalRemainingAmount += rowData.Invoice.InvoiceAmount - rowData.Invoice.TotalPaid;
                
            }
        }

        [BindProperty]
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();

        private CallContext GetCallContext()
        {
            return new CallContext("Iftikhar", "asfasfdds", "asdfasfsd",
                Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }

        public void OnPost(int btnUpdateInvoice)
        {

            var invoice = Invoices[btnUpdateInvoice];
            InvoiceManager mgr = new InvoiceManager();
            mgr.UpdateInvoice(new UpdateInvoiceInput()
            {
                InvoiceId = invoice.InvoiceId,
                IsPaid = invoice.IsPaid,
                PaidAmount = invoice.TotalPaid
            });

            BuildPageData();
        }

      


    }

    

    public class DataModel
    {
        public List<RowData> RowData;
        public decimal TotalInvoiceAmount { get; set; }
        public decimal TotalRemainingAmount { get; set; }
    }

    public class RowData
    {
        public Invoice Invoice { get; set; }
        public MA.Common.Entities.Contacts.Contact FatherContact { get; set; }
        public MA.Common.Entities.Contacts.Contact MotherContact { get; set; }
        public string ResponsiblePartyName { get; set; }
    }
}
