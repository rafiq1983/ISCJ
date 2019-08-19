using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using ISCJ.Pages.PageModels;
using MA.Common;
using MA.Common.Entities.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{
    public class RegisterStudentModel : PageModel
    {
    #region "Methods"
        public void OnGet()
    {
      ProgramManager mgr = new ProgramManager();
      Programs = mgr.GetPrograms();
    }
    public void OnPost()
    {
      Message = "Your information has been submitted";
          
    }
    #endregion
        #region "Properties"
        public string Message { get; set; }
        public List<ProgramDetail> Programs { get; set; }
        [BindProperty]
        public RegisterStudentViewModel Registeration { get; set; }
    }
    #endregion
}

