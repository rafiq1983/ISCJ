using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudentModel : PageModel
    {
    public void OnGet()
    {
      ProgramManager mgr = new ProgramManager();
            Programs = mgr.GetAllPrograms(new MA.Core.CallContext("Iftikhar", "2424", "asfasf", Guid.Empty));
    }

    public void OnPost()
    {
      Message = "Your information has been submitted";
      

      
    }

    public string Message { get; set; }

    public List<ProgramDetail> Programs { get; set; }
  
}
}
