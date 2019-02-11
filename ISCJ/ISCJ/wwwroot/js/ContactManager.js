(function () {
  alert('here');
  var ContactManager = function () {

    var GetContacts = function () {

      return [{ "FirstName": "Mohaib" }, { "FirstName": "Wasi" }];

    }
    ContactManager.prototype.GetAllContacts = function () {
      return GetContacts();
    };

  }


  document.ContactManager = new ContactManager();

}(document, "dev"));
