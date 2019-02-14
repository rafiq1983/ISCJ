using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudent2Model : PageModel
    {
     ProgramManager mgr = new ProgramManager();

    public RegisterStudent2Model()
    {
      Programs = mgr.GetPrograms();
    }
    public void OnGet()
    {

      ;
    }

    public void OnPost()
    {
      //validation.
      if(ModelState.IsValid)
      {
        BusinessLogic.RegistrationManager mgr = new RegistrationManager();
        mgr.CreateRegistration(StudentRegistration);
      }

      Response.Redirect("Registrations");
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
    public CreateRegistrationInput StudentRegistration
    {
      get;
      set;
    }
    }

  
}
