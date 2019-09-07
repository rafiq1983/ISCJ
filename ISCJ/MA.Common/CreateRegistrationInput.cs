﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using MA.Common.Entities.Product;

namespace MA.Common
{
 
  public class CreateRegistrationApplicationInput
  {

      [Required(AllowEmptyStrings = false,
          ErrorMessage = "Father must be selected when creating a registration application.")]
      public Guid? FatherId { get; set; } = null;
      [Required(AllowEmptyStrings = false,
          ErrorMessage = "Mother must be selected when creating a registration application.")]
        public Guid? MotherId { get; set; } = null;
    [Required(AllowEmptyStrings = false, ErrorMessage="Program must be selected when creating a registration application.")]
    public Guid ProgramId { get; set; }
    public bool AddSchoolMemberShipFee { get; set; }
    public bool AddStudentRegistrationFee { get; set; }
    public int StudentRegistrationFeeCount { get; set; }
    public List<CreateStudentRegistrationInput> StudentRegistrations { get; set; }
    public List<ProductSelected> BillingInstructions { get; set; }
  }

  public class CreateStudentRegistrationInput
  {
      [Required(AllowEmptyStrings = false, ErrorMessage="Student must be selected.")]
    public Guid? StudentId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Islamic school grade must be selected.")]
        public string IslamicSchoolGrade { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Public school grade must be selected.")]
        public string PublicSchoolGrade { get; set; }

    public string StudentName { get; set; }

  }

  public class AddRegistrationInput
  {
      public Guid RegistrationApplicationId { get; set; }

      public Guid StudentId { get; set; }
      public string IslamicSchoolGrade { get; set; }
      public string PublicSchoolGrade { get; set; }
      public List<ProductSelected> BillingInstructions { get; set; }
    }

  public class AddRegistrationOutput
  {
      public bool Success { get; set; }
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
