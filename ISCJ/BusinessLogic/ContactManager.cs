using System;
using System.Collections.Generic;
using System.Text;
using MA.Common;

namespace BusinessLogic
{
  public class ContactManager
  {
    public string CreateContact(int userId, CreateContactInput input)
    {
      //TODO: logic to save here.
      return string.Empty;

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

  }
}
