using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common;

namespace BusinessLogic
{
  public class ContactManager
  {
    //TEmp;
    static List<Contact> _Contacts = new List<Contact>();

    static ContactManager()
    {
      var c = new Contact()
      {
        Id = "123",
        FirstName = "Mohaib",
        LastName = "Ahmed",
        CellPhone = "1111-1111-1111",
        HomePhone = "",
        DOB = DateTime.Parse("1/1/1993"),
        State = "NJ",
        Street = "Anthony Drive",
        Zip = "08016",
        City = "Burlington",
        Email = "mohaib@gmail.com",
        Gender = "1"
      };

      _Contacts.Add(c);

      c = new Contact()
      {
        Id = "223",
        FirstName = "Wasi",
        LastName = "Ahmed",
        CellPhone = "222-1111-1111",
        HomePhone = "",
        DOB = DateTime.Parse("1/1/1995"),
        State = "NJ",
        Street = "1 Bloomer Drive",
        Zip = "08016",
        City = "Burlington",
        Email = "wasi@hotmail.com",
        Gender = "1"

      };

      _Contacts.Add(c);
    }
    public string AddUpdateContact(int userId, Contact input)
    {
      //TODO: logic to save here.
      if(string.IsNullOrEmpty(input.Id)==false)
      {
        int indexToUpdate = _Contacts.IndexOf(_Contacts.Single(x => x.Id == input.Id));
        _Contacts[indexToUpdate] = input;
       
      }
      else
      {
        input.Id = Guid.NewGuid().ToString();
        _Contacts.Add(input);
      }
      return input.Id;

    }

    public bool DeleteContact(int userId, string contactId)
    {
      return false;
    }

    public bool AddStudentAssociation(int userId, string studentId, string associationId, string relationType)
    {
      return false;
    }

    public bool UpdateContact(int userId, UpdateContactInput input)
    {
      return false;
    }


    public Contact GetContact(string contactId)
    {
      return new Contact()
      {
        Id = contactId,
        FirstName = "Wasi",
        LastName = "Ahmed",
        CellPhone = "222-1111-1111",
        HomePhone = "",
        DOB = DateTime.Parse("1/1/1995"),
        State = "NJ",
        Street = "1 Bloomer Drive",
        Zip = "08016",
        City = "Burlington",
        Email = "wasi@hotmail.com",
        Gender = "1"
        

      };
    }
    public List<Contact> GetContacts(int userId, int pageNumber, int pageSize)
    {

      return _Contacts;
    }

  }
}
