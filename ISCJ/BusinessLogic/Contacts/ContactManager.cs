using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;

namespace BusinessLogic
{
    public class ContactManager
    {
     static ContactManager()
    {
      using (ContactContext ctx = new ContactContext())
      {

        CacheService.AddData(CacheService.ContactTypesKey, ctx.ContactTypes.ToList());
      }
    }
       
    public static List<ContactType> GetContacTypes()
    {
      return CacheService.GetData(CacheService.ContactTypesKey) as List<ContactType>;
    }
        public string AddUpdateContact(Contact input)
        {
      using (var _ContextContact = new ContactContext())
      {
       
        _ContextContact.Contacts.Add(input);
        if (input.Guid == Guid.Empty)
        {
          input.Guid = Guid.NewGuid();
          _ContextContact.Entry(input).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
        else
          _ContextContact.Entry(input).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _ContextContact.SaveChanges();

        return input.Guid.ToString();
      }

    

        }

        public bool SaveBulkContact(List<Contact> contacts)
        {
            foreach (Contact c in contacts)
            {
        ;
            }

            return true;
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


        public Contact GetContact(Guid contactId)
        {
      using (var _ContextContact = new ContactContext())
      {
       return _ContextContact.Contacts.SingleOrDefault(x => x.Guid == contactId);
       
      }

    }
    public List<Contact> GetContacts(int userId, int pageNumber, int pageSize)
    {
     
      using (var _ContextContact = new ContactContext())
      {
        return _ContextContact.Contacts.ToList();

      }
    }

    }
}
