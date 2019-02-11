
using System;

namespace MA.Common
{
 
  public class Contact
  {
 

        public Contact()
            {
                ModifiedBy="Rafiq";
                ModifiedDate=DateTime.Now;
                CreatedBy="Rafiq";
                CreatedDate=DateTime.Now;
                Guid=Guid.NewGuid();
                ContactType=1;
            }


    public Guid Guid { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public int Gender { get; set; }
    public  string CompanyName { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string State { get; set; }
    public int ContactType {get;set;}
    public Boolean IsParent {get;set;}
    public string Organization {get;set;} 
    public DateTime CreatedDate {get;set;}
    public string CreatedBy{get;set;}
    public DateTime ModifiedDate {get;set;}
    public string ModifiedBy {get;set;}
    public string Email { get; set; }
    public DateTime DOB { get; set; }
    public string Apt { get; set; }
    public string CellPhone { get; set; }
    public string HomePhone { get; set; }

  }

  public class UpdateContactInput
  {
    public string ContactID { get; set; }

    public string CellPhoneNumber { get; set; }
  }

  
}
