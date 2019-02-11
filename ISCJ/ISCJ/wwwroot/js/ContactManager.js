(function () {
  
  var ContactManager = function () {

    var GetContacts = function () {

      return [{ "FirstName": "Mohaib" }, { "FirstName": "Wasi" }];

    }


    ContactManager.prototype.GetAllContacts = function () {
      return GetContacts();
    };

    ContactManager.prototype.CancelSaveContact = function (contact) {

      alert("Cancel Save Contact Called");
      return false;
    }

  }

  document.ContactManager = new ContactManager();

}(document, "dev"));
