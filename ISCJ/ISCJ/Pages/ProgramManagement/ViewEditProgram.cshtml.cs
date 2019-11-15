using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ProgramManagement
{
    public class ViewEditProgramModel : PageModel
    {
        public void OnGet()
        {

        }

        private CallContext GetCallContext()
        {
         return   new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

        public void OnPostSave()
        {
            ProgramManager mgr = new ProgramManager();

            var program = mgr.GetProgramByname(GetCallContext(), ProgramName);

            if (program != null)
            {
                ModelState.AddModelError("ProgramName", "Program with this name already exists");
            }
            else
            {
                mgr.AddProgram(GetCallContext(), ProgramName, ProgramDescription);
                Response.Redirect("programList");
               
            }
        }

        public void OnPostReset()
        {
            ProgramName = string.Empty;
            ProgramDescription = string.Empty;
            ModelState.Clear();

        }

        public void OnPostCancel()
        {
           Response.Redirect("ProgramList");

        }


        [BindProperty] public string ProgramName { get; set; }
        [BindProperty] public string ProgramDescription { get; set; }
    }
}