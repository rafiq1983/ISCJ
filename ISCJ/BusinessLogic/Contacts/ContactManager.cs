using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;
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

    public List<ContactGroup> GetContactGroups(CallContext callContext)
    {
        using (var db = new Database())
        {
            return db.ContactGroups.Where(x => x.TenantId == callContext.TenantId).ToList();
        }
    }

    public Guid CreateContactGroup(CallContext context, CreateUpdateContactGroupInput input)
    {
        using (var db = new Database())
        {
            var contactGroup = new ContactGroup()
            {
                CreateDate = DateTime.UtcNow,
                CreateUser = context.UserLoginName,
                GroupId = Guid.NewGuid(),
                GroupName = input.GroupName,
                TenantId = context.TenantId.Value
            };
            db.ContactGroups.Add(contactGroup);
            db.SaveChanges();
            return contactGroup.GroupId;
        }
    }

        public Guid AddUpdateContact(CallContext callContext, SaveContactInput input)
        {
         using (var _ContextContact = new ContactContext())
         {
             Contact contact = null;
             if (input.Guid == Guid.Empty) //new record.
             {
                 contact = new Contact();
                 contact.Guid = Guid.NewGuid();
                 contact.CreatedDate = DateTime.UtcNow;
                 contact.CreatedBy = callContext.UserLoginName;
                 contact.TenantId = callContext.TenantId.Value;
                 contact.GroupId = input.GroupId;
                 _ContextContact.Contacts.Add(contact);
                 _ContextContact.ContactTenants.Add(new ContactTenant()
                 {
                     TenantId = callContext.TenantId.Value,
                     ContactId = contact.Guid,
                     AssociationName = "TenantContact",
                     CreateUser = callContext.UserLoginName,
                     CreateDate = DateTime.UtcNow

                 });
             }
                else
             {
                 contact = _ContextContact.Contacts.Single(x =>
                     x.TenantId == callContext.TenantId && x.Guid == input.Guid);

                
                     contact.ModifiedDate = DateTime.UtcNow;
                     contact.ModifiedBy = callContext.UserLoginName;
                     _ContextContact.Contacts.Update(contact);
                 
             }


             contact.Apt = input.Apt;
             contact.CellPhone = input.CellPhone;
             contact.City = input.City;
             contact.CompanyName = input.CompanyName;
             contact.ContactType = input.ContactType;

             contact.DOB = input.DOB;
             contact.Email = input.Email;
             contact.StreetAddress = input.StreetAddress;
             contact.Gender = input.Gender;
             contact.State = input.State;
             contact.ZipCode = input.ZipCode;
             contact.FirstName = input.FirstName;
             contact.LastName = input.LastName;
             contact.MiddleName = input.MiddleName;
             contact.HomePhone = input.HomePhone;

             _ContextContact.SaveChanges();
         
                {
                    //Updating all fields of contact.
                    //THE below will do a read and update.  Use attach method if want to modify only a subset of fields.
                    /*_ContextContact.Entry
                        (_ContextContact.Contacts
                            .FirstOrDefaultAsync(x => x.Guid == input.Guid && x.TenantId == callContext.TenantId)
                            .Result)
                        .CurrentValues.SetValues(input)); //will do property matching. Since I don't have modified date/user I don't want to use this.
                    */

                    

                    //If SUbset of fields 
                    /*context.Attach(person);
context.Entry(person).Property("Name").IsModified = true;
context.SaveChanges();*/
                }
                

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
            contact.HomePhone = inputContact.HomePhone;
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


        public Contact GetContact(CallContext context, Guid contactId)
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

        public List<Contact> GetAllContacts(CallContext context)
        {

            using (var _ContextContact = new ContactContext())
            {
                return _ContextContact.Contacts.Where(x => x.TenantId == context.TenantId).ToList();

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
