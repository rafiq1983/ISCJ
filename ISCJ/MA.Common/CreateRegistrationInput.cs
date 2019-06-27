using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Product;

namespace MA.Common
{
 
  public class CreateRegistrationInput
  {
    public Guid FatherId { get; set; }
    public Guid MotherId { get; set; }
    public Guid ProgramId { get; set; }
    public bool AddSchoolMemberShipFee { get; set; }
    public bool AddStudentRegistrationFee { get; set; }
    public int StudentRegistrationFeeCount { get; set; }
    public List<CreateStudentRegistrationInput> StudentRegistration { get; set; }
    public List<ProductSelected> BillingInstructions { get; set; }
  }

  public class CreateStudentRegistrationInput
  {
    public Guid? StudentId { get; set; }
    public string IslamicSchoolGrade { get; set; }
    public string PublicSchoolGrade { get; set; }
  }

  public class RegistrationDetail
  {
    public Guid RegistrationId { get; set; }
  }

  public class ProductSelected
  {
      public Guid ProductId { get; set; }
      public int ProductCount { get; set; }
      public bool IsSelected { get; set; }
      public string ProductCode { get; set; }
  }
}
