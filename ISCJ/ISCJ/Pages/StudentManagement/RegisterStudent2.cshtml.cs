using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudent2Model : PageModel
    {
     ProgramManager mgr = new ProgramManager();
     private ProductManager productMgr = new ProductManager();

    public RegisterStudent2Model()
    {
        Products = productMgr.GetAllProducts(GetCallContext());
        StudentRegistration = BuildForDisplay();
    }

    private CreateRegistrationApplicationInput BuildForDisplay()
    {
       var output = new CreateRegistrationApplicationInput();
       
        output.StudentRegistrations = new List<CreateStudentRegistrationInput>();
        output.StudentRegistrations.Add(new CreateStudentRegistrationInput()
        {
             
        });

        output.StudentRegistrations.Add(new CreateStudentRegistrationInput()
        {

        });

        output.StudentRegistrations.Add(new CreateStudentRegistrationInput()
        {

        });

        output.StudentRegistrations.Add(new CreateStudentRegistrationInput()
        {
            
        });

            return output;
    }
    private CallContext GetCallContext()
    {
        return new CallContext("Iftikhar", "234234", "askfj", Guid.Parse("697400B2-8AA0-4F01-A282-E58530DBC2A8"));
        }


    public void OnGet()
    {
        
           
    }

    private void ValidateStudents()
    {
        if (StudentRegistration.StudentRegistrations.Count == 0)
        {
            ModelState.AddModelError<CreateRegistrationApplicationInput>((x)=>x.StudentRegistrations[0].StudentId, "Atleast one student registration must be selected.");
        }

        for (int i = 0; i < StudentRegistration.StudentRegistrations.Count; i++)
        {
            bool runStudentValidation = i == 0 || StudentRegistration.StudentRegistrations[i].StudentId.HasValue;


                if (runStudentValidation && StudentRegistration.StudentRegistrations[i].StudentId.HasValue == false)
                {
                    ModelState.AddModelError<CreateRegistrationApplicationInput>(
                       (x) => x.StudentRegistrations[i].StudentId, "Student must be selected.");

                  
                }

                if (runStudentValidation)
                {
                    if (string.IsNullOrEmpty(StudentRegistration.StudentRegistrations[i].PublicSchoolGrade))
                    {
                        ModelState.AddModelError<CreateRegistrationApplicationInput>(
                            (x) => x.StudentRegistrations[i].StudentId,
                            "Student public school grade must be selected.");

                    }

                    if (string.IsNullOrEmpty(StudentRegistration.StudentRegistrations[i].IslamicSchoolGrade))
                    {
                        ModelState.AddModelError("StudentRegistration.StudentRegistrations[{i}].IslamicSchoolGrade",
                            "Student Islamic school grade must be selected.");

                    }
                }
        }
     }

    private void EnsureStudentDoesNotBelongToThisProgramAlready()
    {
        BusinessLogic.RegistrationManager mgr = new RegistrationManager();

        if (ModelState.IsValid)
        {
            var enrollments = mgr.GetEnrollments(StudentRegistration.ProgramId.GetValueOrDefault(Guid.Empty));

            for (int i = 0; i < StudentRegistration.StudentRegistrations.Count; i++)
            {
                var enrollment = enrollments.SingleOrDefault(x =>
                    x.StudentContactId == StudentRegistration.StudentRegistrations[i].StudentId
                        .GetValueOrDefault(Guid.Empty)
                    && x.ProgramId == StudentRegistration.ProgramId.GetValueOrDefault());

                if (enrollment != null)
                {
                        ModelState.AddModelError<CreateStudentRegistrationInput>(x=>x.StudentId,"Student " + StudentRegistration.StudentRegistrations[i].StudentName + " is already enrolled in this program.");

                }
            }
        }

    }
    public void OnPostSave()
    {
     
        for (int i = 1; i < StudentRegistration.StudentRegistrations.Count; i++)
        {
            if (StudentRegistration.StudentRegistrations[i].StudentId.HasValue == false) //don't do validation on Student 2, 3, 4 if no student is selected.
            {
                //Clear model validation state only clears the state.  But Error stays.  So, clearing the errors.
               ModelState.ClearValidationState($"StudentRegistration.StudentRegistrations[{i}].StudentId");
                ModelState.ClearValidationState($"StudentRegistration.StudentRegistrations[{i}].PublicSchoolGrade");
                ModelState.ClearValidationState($"StudentRegistration.StudentRegistrations[{i}].IslamicSchoolGrade");
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].StudentId"].Errors.Clear();
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].PublicSchoolGrade"].Errors.Clear();
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].IslamicSchoolGrade"].Errors.Clear();
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].IslamicSchoolGrade"].ValidationState =
                    ModelValidationState.Skipped;
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].PublicSchoolGrade"].ValidationState =
                    ModelValidationState.Skipped;
                ModelState[$"StudentRegistration.StudentRegistrations[{i}].StudentId"].ValidationState =
                    ModelValidationState.Skipped;
            }

            }

        EnsureStudentDoesNotBelongToThisProgramAlready();

            if (ModelState.IsValid)
            {
                RegistrationManager mgr = new RegistrationManager();
                mgr.CreateRegistration(GetCallContext(), StudentRegistration);
        Response.Redirect("Enrollments");
      }
      else
      {
          Products = productMgr.GetAllProducts(GetCallContext());
          //StudentRegistration = BuildForDisplay();
      }

    }

    public void OnPostRemove(int btnStudentRemove)
    {
       
        if (StudentRegistration.StudentRegistrations.Count >btnStudentRemove)
        {
            StudentRegistration.StudentRegistrations.RemoveAt(btnStudentRemove);
        }

        Products = productMgr.GetAllProducts(GetCallContext());
        ModelState.Clear();
    }

    public void OnPostCancel()
    {
        ModelState.Clear();
        Products = productMgr.GetAllProducts(GetCallContext());
        }

    public void OnPostReset()
    {
        ModelState.Clear();
        Products = productMgr.GetAllProducts(GetCallContext());
        }


        public IEnumerable<SelectListItem> PublicSchoolGradeList
    {
      get
      {
        return ListService.GetPublicSchoolGradeList().Select(x => new SelectListItem(x.GradeName, x.GradeId));
      }
    }

    public IEnumerable<SelectListItem> IslamicSchoolGradeList
    {
      get
      {
        return ListService.GetIslamicSchoolGradesList().Select(x => new SelectListItem(x.GradeName, x.GradeId));
      }
    }

    public IEnumerable<SelectListItem> Programs
    {
        get
        {
           return mgr.GetAllPrograms(GetCallContext()).Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString()));
              
        }
    }


    [BindProperty]
    public List<MA.Common.Entities.Product.BillableProduct> Products { get; set; }

    [BindProperty]
    public CreateRegistrationApplicationInput StudentRegistration
    {
      get;
      set;
    }

    public List<ContactType> ContactTypes { get { return ContactManager.GetContacTypes(); } }


    }

  
}
