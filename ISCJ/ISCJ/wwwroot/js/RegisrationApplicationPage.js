   $("document").ready(function() {
            SetupContactAutoComplete();


            $("#btnAddNewContact").click(function (evt) {
                var modalId = evt.currentTarget.attributes["data-target"].value;

                $(modalId + " #olErrors").empty();
                $(modalId + " #dvErrorsRow").css("display", "none");
               
               
                $(modalId + " .modal-body").find('input').each(function() {
                    $(this).val("");

                });

                $(modalId + " .modal-body").find('select').each(function() {
                    $(this).val("1");

                });

            });
            $("#btnSave").click(function() {
                var personAddress = {
                    AddressLine1: $("#txtstreet").val(),
                    AddressLine2: "",
                    City: $("#txtcity").val(),
                    StateCode: $("#txtstate").val(),
                    ZipCode: $("#txtzip").val(),
                    CountryCode: ""
                }
                var contact = {
                    FirstName: $("#txtfirstname").val(),
                    LastName: $("#txtlastname").val(),
                    Address: personAddress,
                    HomePhone: $("#txthomephone").val(),
                    CellPhone: $("#txtcellphone").val(),
                    MiddleName: $("#txtmiddlename").val(),
                    Email: $("#txtemail").val(),
                    ContactType: $("#slContactTypes").val(),
                    Gender: $("#slgender").val()
                }

                var AddContactInputObj = {
                    Contact: contact
                }
                var p = JSON.stringify(AddContactInputObj);
                var pagePath = window.location.origin;
                pagePath += "/api/Contact";
                $.ajax({
                    type: "POST",
                    url: pagePath,
                    contentType: "application/json; charset=utf-8",
                    data: p,
                    dataType: "json",
                    success: function(d) {
                        $("#olErrors").empty();
                        $('#addNewContactModal').find('input:text').val('');
                        $('#addNewContactModal').modal('hide');
                    },
                    error: function(response) {
                        $("#olErrors").empty();
                        $("#dvErrorsRow").css("display", "block");
                        if (response.status === 400) {
                            $("#olErrors").empty();
                            var errorobj = JSON.parse(response.responseText);
                            $.each(errorobj,
                                function(i, val) {
                                    $("#olErrors").append($("<li>").text(val.errorMessage));
                                });
                        } else {
                            alert("Error");
                        }
                    }
                });


            });

        });


function ConfigureTypeAhead() {
    var cars = ['Audi', 'BMW', 'Bugatti', 'Ferrari', 'Ford', 'Lamborghini', 'Mercedes Benz', 'Porsche', 'Rolls-Royce', 'Volkswagen'];

    // Constructing the suggestion engine
    var cars = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        local: cars
    });

    // Initializing the typeahead
    $('.typeahead').typeahead({
            hint: true,
            highlight: true, /* Enable substring highlighting */
            minLength: 1 /* Specify minimum characters required for showing suggestions */
        },
        {
            name: 'cars',
            source: cars
        });
}
function SetupContactAutoComplete() {

    ConfigureTypeAhead();


            $(".autocomplete").autoComplete({
                minLength: 1,
                formatResult: function(item) {
                    console.log("Format result called with item " + item);
                    return {
                        id: item.guid,
                        text: item.firstName + " " + item.lastName
                    }
                },

                events: {
                    searchPost: function(resultsFromServer) {
                        console.log("Search Post Called. " + resultsFromServer[0].guid);
                        return resultsFromServer;
                    }
                }

            });

            $(".autocomplete").on('autocomplete.select',
                function(evt, item) {
                    $("#" + evt.currentTarget.attributes["data-target-id"].value).val(item.guid);
                    SetIsMemberFlag(evt.currentTarget.attributes["data-member-check-id"].value, item.guid);
                    console.log(evt.currentTarget.attributes["data-target-id"].value);
                    console.log("added item " + item.guid);
                    evt.preventDefault();
                });

            $(".autocomplete").change(function(evt) {
                console.log("Textbox change");
                console.log($("#" + evt.currentTarget.attributes["data-target-id"].value));
                console.log($("#" + evt.currentTarget.attributes["data-target-id"].value).val());

                $("#" + evt.currentTarget.attributes["data-target-id"].value).val("");

            });
        }

        function SetIsMemberFlag(memberCheckId, id) {
            if (memberCheckId == null || memberCheckId == "")
                return;
            $(".FatherEnrollmentFound").hide();
            $(".MotherEnrollmentFound").hide();

            $.ajax({
                type: "GET",
                url: "/api/membership/membershipbycontact/" + id,
                contentType: "application/json; charset=utf-8",

                dataType: "json",
                success: function (d) {
                   
                    console.log(d);
                    $("#"+ memberCheckId).text(d != null);
                },
                error: function(response) {
                    console.Log("Error getting member status " + response);
                }
            });


            $.ajax({
                type: "GET",
                url: "/api/Enrollment/get-by-parent/" + id,
                contentType: "application/json; charset=utf-8",

                dataType: "json",
                success: function (d) {
                   
                    console.log(d);
                    if (d.length > 0) {
                        _enrollments = d;
                        $(".FatherEnrollmentFound").show();
                        $(".MotherEnrollmentFound").show();
                        $(".MotherEnrollmentFound,.FatherEnrollmentFound").on("click", function ()
                        {
                            DisplayEnrollmentsFound();
                        })
                         
                    }
                },
                error: function(response) {
                    console.Log("Error getting member status " + response);
                }
            });

        }

        $("#btnPopulateEnrollment").click(function (evt)
        {

            evt.preventDefault();
            _enrollments = [];
            var i = 0;            
            $(".enrollment:checkbox:checked").each(function ()
            {                  
                var enr = $(this).data("enrollment") 
                var obj=JSON.parse(decodeURIComponent(enr));                
                _enrollments[i] = obj;
                i = i + 1;               
            });

            if (i > 0)
            {
                $("#AutoEnrollmentJson").val(JSON.stringify(_enrollments));
                $("#btnPostSelectedEnrollments").click();
                $("#enrollmentsFoundModal").modal("hide");
            }
            else
            {
                $("#dvenrollmentErrors").css("display", "block");
                
            }
        });

        function DisplayEnrollmentsFound()
        {
            $("#dvenrollmentErrors").css("display", "none");
            $("#tblEnrollments > tbody").empty();
            $.each(_enrollments, function (i, val)
            {               
                var StudentName = val.studentContactInfo.firstName + " " + val.studentContactInfo.lastName;
                var publicSchoolGradeId = val.publicSchoolGradeId;
                var islamicSchoolGradeId = val.islamicSchoolGradeId;
                $("#tblEnrollments > tbody").append('<tr><td>' + StudentName + '</td>' + '<td>' + publicSchoolGradeId + '</td> ' + '<td>' + islamicSchoolGradeId + '</td> <td><input class=enrollment data-enrollment=' + encodeURIComponent(JSON.stringify(val)) + ' type="checkbox" /></td> </tr>');
            });
                  
            $("#enrollmentsFoundModal").modal();
        }

        var _enrollments;