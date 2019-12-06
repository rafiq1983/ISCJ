using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public Guid AddUpdateContact(CallContext callContext, SaveContactInput input)
        {
         using (var _ContextContact = new ContactContext())
         {
                if (input.Guid == Guid.Empty) //new record.
                {
                    Contact contact = new Contact()
                    {
                        Apt = input.Apt,
                        CellPhone = input.CellPhone,
                        City = input.City,
                        CompanyName = input.CompanyName,
                        ContactType = input.ContactType,

                        DOB = input.DOB,
                        Email = input.Email,
                        TenantId = callContext.TenantId.Value,
                        StreetAddress = input.StreetAddress,
                        Gender = input.Gender,
                        State = input.State,
                        ZipCode = input.ZipCode,
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        MiddleName = input.MiddleName,
                        Guid = Guid.NewGuid(),
                        CreatedBy = callContext.UserLoginName,
                        CreatedDate = DateTime.UtcNow
                    };

                 _ContextContact.Contacts.Add(contact);
             }
                else
                {
                    //Updating all fields of contact.
                    //THE below will do a read and update.  Use attach method if want to modify only a subset of fields.
                    /*_ContextContact.Entry
                        (_ContextContact.Contacts
                            .FirstOrDefaultAsync(x => x.Guid == input.Guid && x.TenantId == callContext.TenantId)
                            .Result)
                        .CurrentValues.SetValues(input)); //will do property matching. Since I don't have modified date/user I don't want to use this.
                    */

                    var updatedContact = _ContextContact.Contacts.SingleOrDefault(x => x.TenantId == callContext.TenantId && x.Guid == input.Guid);
                    if (updatedContact != null)
                    {
                        updatedContact.ModifiedDate = DateTime.UtcNow;
                        updatedContact.ModifiedBy = callContext.UserLoginName;
                    }

                    //If SUbset of fields 
                    /*context.Attach(person);
context.Entry(person).Property("Name").IsModified = true;
context.SaveChanges();*/
                }

                _ContextContact.SaveChanges();

             return input.Guid;
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
            contact.CreatedBy = callContext.UserLoginName;
            contact.CreatedDate = System.DateTime.UtcNow;
            contact.Guid = Guid.NewGuid();
            contact.ContactType = input.Contact.ContactType;
            contact.CreatedBy = callContext.UserLoginName;
            contact.CreatedDate = DateTime.UtcNow;
            contact.TenantId = callContext.TenantId.Value;
            contact.Gender = input.Contact.Gender;
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
    public List<Contact> GetContacts(CallContext context, int userId, int pageNumber, int pageSize)
    {
     
      using (var _ContextContact = new ContactContext())
      {
          return _ContextContact.Contacts.Where(x => x.TenantId == context.TenantId).ToList();

      }
    }

        public List<Contact> GetAllContacts()
        {

            using (var _ContextContact = new ContactContext())
            {
                return _ContextContact.Contacts.ToList();//.Where(x => x.Guid == Guid.Parse("6358BD29-A24B-4294-9D5C-00CD2B3606A7")).ToList();

            }
        }

        public List<Contact> SearchContactByPrefix(CallContext callContext, string prefix)
        {


            using (var _ContextContact = new ContactContext())
            {
                var contacts = _ContextContact.Contacts.Where(x => x.TenantId == callContext.TenantId);
                contacts = contacts.Where(x =>
                    (x.FirstName.ToLower() + " " + x.LastName.ToLower()).IndexOf(prefix.ToLower()) >= 0);
                return contacts.ToList();
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
