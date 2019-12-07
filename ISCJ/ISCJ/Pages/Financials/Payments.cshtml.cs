using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessLogic;
using ISCJ.Migrations;
using ISCJ.Pages.ContactManagement;
using MA.Common;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.Payments;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Swashbuckle.AspNetCore.Swagger;

namespace ISCJ.Pages.Financials
{
    public class PaymentsModel: BasePageModel
    {
        readonly PaymentsManager _paymentManager = new PaymentsManager();
        private readonly ContactManager _contactManager = new ContactManager();
        private readonly RegistrationManager _registrationManager = new RegistrationManager();

        public DataModel PageData = new DataModel();
        public void OnGet()
        {
            BuildPageData();

        }

        private void BuildPageData()
        {
            var payments = _paymentManager.GetAllPayments(GetCallContext());

            var contacts = _contactManager.GetAllContacts(GetCallContext());

             PageData.RowData = new List<RowData>();

            foreach (AllPayment payment in payments)
            {
                RowData rowData = new RowData();
                rowData.PaymentAmount = payment.PaymentAmount;
                rowData.PaymentDate = payment.PaymentDate;
                rowData.PaymentId = payment.PaymentId;
                var contact = contacts.SingleOrDefault(x => x.Guid == payment.PayorId);
                rowData.PaymentMadeBy = GetContactName(contact);
                rowData.PaymentMadeById = payment.PayorId;
                rowData.InvoiceId = payment.InvoiceId;
                rowData.PaymentMethod = payment.PaymentMethod.ToString();
               
            PageData.RowData.Add(rowData);
            }

        }


    
    }

    public class DataModel
    {
        public List<RowData> RowData;
       
    }

    public class RowData
    {
        public Guid PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMadeBy { get; set; }
        public Guid PaymentMadeById { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public Guid? InvoiceId { get; set; }
    }
}
