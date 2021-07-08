

//"LaunchSelectInvoice"

function LoadInvoices() {

    var contactId = $("#ContactId").val();

    if (contactId == null || contactId == "") {
        alert("Select Contact First");
    }
    $.ajax({
        type: "GET",
        url: "/api/Invoice/responsibleparty/" + $("#ContactId").val(),
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (d) {
           
            console.log(d);
            if (d.length > 0) {
                _invoices = d;
                DisplayInvoicesModal();

            }
        },
        error: function (response) {
            console.log("Error getting member status " + response);
        }
    });

}
/* "financialAccount": null,
    "invoiceId": "d351cb1f-c0e4-4118-58e3-08d77c4d1902",
    "invoiceAmount": 45,
    "tennantId": "cd9dd164-6c1e-4fe3-82ac-341df0ac3453",
    "financialAccountId": "00000000-0000-0000-0000-000000000000",
    "invoiceTypeId": "8c112591-4c5f-45d5-9e2e-24019017d695",
    "dueDate": "2019-12-09T00:00:00",
    "responsiblePartyId": "86c89cdd-9882-4574-3565-08d76f8f2244",
    "description": "Invoice generated during registration application.",
    "generationDate": "2019-12-09T00:00:00",
    "totalPaid": 0,
    "isPaid": false,
    "invoiceItems": null,
    "referenceId": "e5ad1cfc-60f8-4bde-b61a-e06f81142723",
    "referenceType": 1,
    "createDate": "2019-12-09T02:11:27.007",
    "createUser": "i_syed2000@yahoo.com",
    "modifiedDate": null,
    "modifiedUser": null*/


function DisplayInvoicesModal() {
    
    $("#tblEnrollments > tbody").empty();
    $.each(_invoices, function (i, val) {
        console.log(val.duedate);
        var invoiceDate = val.dueDate;
        var invoiceAmount = val.invoiceAmount;
        var responsiblePartyName = val.responsiblePartyId;
        $("#tblEnrollments > tbody").append('<tr><td>' + invoiceDate + '</td>' + '<td>' + invoiceAmount + '</td> ' + '<td>' + val.totalPaid + '</td> ' + '<td>' + responsiblePartyName + '</td> <td><input name="radioInvoice" class=selectedInvoice data-invoice=' + encodeURIComponent(JSON.stringify(val)) + ' type="radio" /></td> </tr>');
    });
    $("#selectInvoiceModalWindow").modal();
}


$("#btnSelectInvoice").click(function (evt) {

    evt.preventDefault();
    _invoices = [];
    var i = 0;
    $(".selectedInvoice:radio:checked").each(function () {
        var enr = $(this).data("invoice");
        var obj = JSON.parse(decodeURIComponent(enr));
        $("#lblSelectedInvoiceId").text(obj.invoiceId);
        $("#InvoiceId").val(obj.invoiceId);
       
        i = i + 1;
    });

    if (i > 0) {
          $("#selectInvoiceModalWindow").modal("hide");
    }
    else {
        $("#dvenrollmentErrors").css("display", "block");

    }
});

var _invoices;