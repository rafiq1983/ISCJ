using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MA.Common.Entities.Contacts;
using MA.Common.Models.api;

namespace MA.Common.Validation
{
   public class ContactValidator: AbstractValidator<ContactApi>
    {

        public ContactValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name is required.");          
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.");           
            RuleFor(p => p.HomePhone).NotEmpty().WithMessage("Home phone is required.");
            RuleFor(p => p.Address).NotNull().WithMessage("Address is Required.");
            RuleFor(p => p.Address.AddressLine1).NotEmpty().WithMessage("Address is required");
            RuleFor(p => p.Address.City).NotEmpty().WithMessage("City is required");
            RuleFor(p => p.Address.StateCode).NotEmpty().WithMessage("State is required");
            RuleFor(p => p.Address.ZipCode).NotEmpty().WithMessage("Zip Code is required");
            RuleFor(p => p.ContactType).NotEmpty().WithMessage("Contact type is required");
            RuleFor(p => p.Gender).NotEmpty().WithMessage("Gender is required.");
        }

    }

   public class StudentContactValidator : AbstractValidator<ContactApi>
   {
       public StudentContactValidator()
       {
           RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name is required.");
           RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name is required.");
           RuleFor(p => p.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(p => p.ContactType).NotEmpty().WithMessage("Contact type is required");
        }

    }
}
