using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudentModel : PageModel
    {
    public void OnGet()
    {

    }

    public void OnPost()
    {
      Message = "Your information has been submitted";
    }

    public string Message { get; set; }
  
}
}
