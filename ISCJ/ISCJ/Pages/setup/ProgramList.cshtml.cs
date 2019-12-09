using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.setup
{
    public class ProgramListModel : BasePageModel
    {
        ProgramManager mgr = new ProgramManager();

        public void OnGet()
        {
            ProgramDetails = mgr.GetAllPrograms(GetCallContext());
        }


        public List<ProgramDetail> ProgramDetails
        {
            get;
            private set;
        }
    }
}