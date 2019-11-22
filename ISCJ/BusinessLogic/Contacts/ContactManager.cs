using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;

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
      var Types= CacheService.GetData(CacheService.ContactTypesKey) as List<ContactType>;

      return Types;
    }
        public string AddUpdateContact(CallContext callContext, Contact input)
        {
         using (var _ContextContact = new ContactContext())
         {
             input.TenantId = callContext.TenantId;
        _ContextContact.Contacts.Add(input);
        _ContextContact.SaveChanges();
        return input.Guid.ToString();
      }

        }

        public AddContactOutput AddNewContact(CallContext callContext,AddContactInput input)
        {
            var inputContact = input.Contact;
            Contact contact = new Contact();
            contact.FirstName = inputContact.FirstName;
            contact.LastName = inputContact.LastName;
            contact.HomePhone = inputContact.HomePhone;
            contact.StreetAddress = inputContact.Address?.AddressLine1;
            contact.Apt = input.Contact.Address?.Apt;
            contact.City = inputContact.Address?.City;
            contact.State = inputContact.Address?.StateCode;
            contact.ZipCode = inputContact.Address?.ZipCode;
            contact.Email = inputContact.Email;
            if (inputContact.DOB.HasValue)
                contact.DOB = inputContact.DOB.Value;
            contact.CellPhone = inputContact.CellPhone;
            contact.CreatedBy = callContext.UserId;
            contact.CreatedDate = System.DateTime.UtcNow;
            contact.Guid = Guid.NewGuid();
            contact.ContactType = input.Contact.ContactType;
            contact.CreatedBy = callContext.UserId;
            contact.CreatedDate = DateTime.UtcNow;

            using (var _ContextContact = new ContactContext())
            {
                _ContextContact.Contacts.Add(contact);
                _ContextContact.SaveChanges();
                return new AddContactOutput()
                {
                    ContactId = contact.Guid
                };
            }

        }

        public bool SaveBulkContact(List<Contact> contacts)
        {
      using (var _ContextContact = new ContactContext())
      {
        foreach (Contact c in contacts)
        {
          if(string.IsNullOrEmpty(c.FirstName)==false)
            _ContextContact.Contacts.Add(c); //this auto assign Guid column.  Not sure why?
        }

        _ContextContact.SaveChanges();
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

        public List<Contact> GetAllContacts()
        {

            using (var _ContextContact = new ContactContext())
            {
                return _ContextContact.Contacts.ToList();//.Where(x => x.Guid == Guid.Parse("6358BD29-A24B-4294-9D5C-00CD2B3606A7")).ToList();

            }
        }
        public List<Contact> GetContactsByContactType(string userId, int contactType)
        {
            using (var _ContextContact = new ContactContext())
            {
                return _ContextContact.Contacts.Where(x => x.ContactType == contactType).ToList();
            }
        }

    }
}
