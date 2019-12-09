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
    public class ViewEditProgramModel : BasePageModel
    {
        public void OnGet()
        {
            LoadPrograms();
        }

        public List<ProgramDetail> ProgramDetails
        {
            get;
            private set;
        }

        public void OnPostSave()
        {
            ProgramManager mgr = new ProgramManager();

            var program = mgr.GetProgramByName(GetCallContext(), ProgramName);

            if (program != null)
            {
                ModelState.AddModelError("ProgramName", "Program with this name already exists");
            }
            else
            {
                mgr.AddProgram(GetCallContext(), ProgramName, ProgramDescription);
               
            }

            LoadPrograms();
        }

        public void OnPostReset()
        {
            ProgramName = string.Empty;
            ProgramDescription = string.Empty;
            ModelState.Clear();
            LoadPrograms();

        }

        public void OnPostCancel()
        {
           Response.Redirect("main");
         

        }

        private void LoadPrograms()
        {
            ProgramManager mgr = new ProgramManager();
            ProgramDetails = mgr.GetAllPrograms(GetCallContext());
        }

        [BindProperty] public string ProgramName { get; set; }
        [BindProperty] public string ProgramDescription { get; set; }
    }
}