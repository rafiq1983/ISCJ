using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MA.Common.Entities.Contacts
{
  public class Contact
  {
    public Contact()
    {
    
    }


    public Guid TenantId { get; set; }
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
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }

       
        [Required]
    public string Email { get; set; }
    public DateTime? DOB { get; set; }
    public string Apt { get; set; }
        [Required]
    public string CellPhone { get; set; }
        [Required]
    public string HomePhone { get; set; }

  }

  public class ContactTenant:BaseEntity
  {
      public Guid ContactId { get; set; }
      public Guid TenantId { get; set; }
      public string AssociationName { get; set; }

  }
}
