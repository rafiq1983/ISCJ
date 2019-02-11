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

        public ContactManager()
        {
           
     
        }
        public string AddUpdateContact(Contact input)
        {
            //TODO: logic to save here.
            if (string.IsNullOrEmpty(input.Guid.ToString()) == false)
            {
                using (var _ContextContact = new ContactContext())
                {
                    _ContextContact.Contacts.Add(input);
                    _ContextContact.SaveChanges();
                }             
                //int indexToUpdate = _Contacts.IndexOf(_Contacts.Single(x => x.Guid.ToString() == input.Guid.ToString()));
                //_Contacts[indexToUpdate] = input;
            }
            else
            {
                input.Guid = Guid.NewGuid();
                _Contacts.Add(input);
            }
            return input.Guid.ToString();

        }

        public bool SaveBulkContact(List<Contact> contacts)
        {
            foreach (Contact c in contacts)
            {
                c.Guid = Guid.NewGuid();
                _Contacts.Add(c);
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


        public Contact GetContact(string contactId)
        {
            return _Contacts.Single(x => x.Guid.ToString() == contactId);
        }
        public List<Contact> GetContacts(int userId, int pageNumber, int pageSize)
        {

            return _Contacts;
        }

    }
}
