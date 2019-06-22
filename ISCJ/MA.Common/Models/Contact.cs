using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models
{

    public class AddContactInput
    {
       public Contact Contact { get; set; }
    }

    #region "Common"
        public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactType { get; set; }
        public int Gender { get; set; }
        public string CompanyName { get; set; }

        public Address Address { get; set; }

        public Phone[] Phone { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }
    }
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        

    }

    public class Phone
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Fax, Cell, Home
    }

    public enum EnumContactType
    {
        Student, Parent, Donor, Teacher, Other
    }

#endregion
}
