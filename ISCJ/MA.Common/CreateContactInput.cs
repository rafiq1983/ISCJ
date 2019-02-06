using Microsoft.AspNetCore.Mvc;
using System;

namespace MA.Common
{
 
  public class Contact
  {
   [BindProperty]
    public string Id { get; set; }

    [BindProperty]
    public string FirstName { get; set; }


    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]

    public string Email { get; set; }

    [BindProperty]
    public string Gender { get; set; }


    [BindProperty]

    public string MiddleInitial { get; set; }

    [BindProperty]
    public DateTime DOB { get; set; }

    [BindProperty]
    public string Street { get; set; }

    [BindProperty]
    public string City { get; set; }

    [BindProperty]
    public string Zip { get; set; }
    [BindProperty]
    public string State { get; set; }
    [BindProperty]
    public string Apt { get; set; }
    [BindProperty]

    public string CellPhone { get; set; }
    [BindProperty]
    public string HomePhone { get; set; }

    [BindProperty]
    public string ContactTypeId { get; set; }
  }

  public class UpdateContactInput
  {
    public string ContactID { get; set; }

    public string CellPhoneNumber { get; set; }
  }

  
}
