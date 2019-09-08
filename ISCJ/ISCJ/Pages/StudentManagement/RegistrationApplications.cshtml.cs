using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{
    public class RegistrationApplicationsModel : PageModel
    {
    ProgramManager programMgr;

    public RegistrationApplicationsModel()
    {
      programMgr = new ProgramManager();

            Programs = programMgr.GetAllPrograms(new MA.Core.CallContext("Iftikhar", "23234", "asfasf", Guid.Empty));
    }
        public void OnGet()
        {
            RegistrationManager mgr = new RegistrationManager();
            Registrations = mgr.GetAllApplications(GetCallContext(), Programs[0].ProgramId);
            if(Programs.Any())
                ProgramId = Programs[0].ProgramId;
        }

        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23234", "asfasf", Guid.Empty);
        }

    public void OnPost()
    {
        RegistrationManager mgr = new RegistrationManager();
        Registrations = mgr.GetAllApplications(GetCallContext(), ProgramId);
    }


    public List<MA.Common.Entities.Registration.RegistrationApplication> Registrations { get; set; } =
        new List<RegistrationApplication>();

    public List<ProgramDetail> Programs { get; set; }

    [BindProperty]
    public Guid ProgramId { get; set; }

  }
}
