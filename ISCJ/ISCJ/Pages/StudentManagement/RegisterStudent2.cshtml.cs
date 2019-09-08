using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudent2Model : PageModel
    {
     ProgramManager mgr = new ProgramManager();
     private ProductManager productMgr = new ProductManager();

    public RegisterStudent2Model()
    {
        Programs = mgr.GetAllPrograms(GetCallContext());
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

    public void OnPostSave()
    {
      //validation.
      if(ModelState.IsValid)
      {
        BusinessLogic.RegistrationManager mgr = new RegistrationManager();
        mgr.CreateRegistration(GetCallContext(), StudentRegistration);
        Response.Redirect("Registrations");
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

    public List<ProgramDetail> Programs { get; set; }
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
