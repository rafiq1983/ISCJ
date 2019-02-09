
using System;

namespace MA.Common
{
 
  public class Contact
  {
 
    public string Id { get; set; }


    public string FirstName { get; set; }



    public string LastName { get; set; }


    public string Email { get; set; }


    public string Gender { get; set; }




    public string MiddleInitial { get; set; }

    public DateTime DOB { get; set; }


    public string Street { get; set; }


    public string City { get; set; }


    public string Zip { get; set; }

    public string State { get; set; }
  
    public string Apt { get; set; }
 

    public string CellPhone { get; set; }

    public string HomePhone { get; set; }

    public string ContactTypeId { get; set; }
  }

  public class UpdateContactInput
  {
    public string ContactID { get; set; }

    public string CellPhoneNumber { get; set; }
  }

  
}
