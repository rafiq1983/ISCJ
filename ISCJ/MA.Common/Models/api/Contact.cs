using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MA.Common.Models.api
{

    public class AddContactInput
    {
        public ContactApi Contact { get; set; }
    }

    public class AddContactOutput
    {
        public Guid ContactId { get; set; }
    }

    public class CreateUpdateContactGroupInput
    {
        public string GroupName { get; set; }
        public Guid? GroupId { get; set; }
    }
    public class SaveContactInput
    {
        public Guid Guid { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public int Gender { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string State { get; set; }
        public int ContactType { get; set; }
        public Boolean IsParent { get; set; }
        public string Organization { get; set; }
      
        [Required]
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Apt { get; set; }
        
        [Phone(ErrorMessage = "Cell Phone must be in ###-###-#### format.")]
        public string CellPhone { get; set; }

        [Phone(ErrorMessage = "Home Phone must be in ###-###-#### format.")]
        public string HomePhone { get; set; }
        public Guid? GroupId { get; set; }
    }
    #region "Common"
        public class ContactApi //TODO: Iftikhar; Renaming it to ContactApi otherwise, swagger fails.  It gets confused between Contact class in Entities vs this.
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       public int ContactType { get; set; }
        public int Gender { get; set; }
        public string CompanyName { get; set; }

        public Address Address { get; set; }

        public string HomePhone { get; set; }
        public string CellPhone { get; set; }

        public string Email { get; set; }

        public DateTime? DOB { get; set; }
    }
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string Apt { get; set; }
        public string ZipCode { get; set; }
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
