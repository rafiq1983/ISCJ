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
    public class RegistrationsModel : PageModel
    {
    ProgramManager programMgr;
    public RegistrationsModel()
    {
      programMgr = new ProgramManager();

            Programs = programMgr.GetAllPrograms(new MA.Core.CallContext("Iftikhar", "23234", "asfasf", Guid.Empty));
    }
        public void OnGet()
        {
            RegistrationManager mgr = new RegistrationManager();
            Guid registrationAppId;
            bool validRegisAppId  = Guid.TryParse(Request.Query["regappid"], out registrationAppId);

            if (validRegisAppId)
                Registrations = mgr.GetRegistrations(Programs[0].ProgramId, registrationAppId);
            else
            {
                Registrations = mgr.GetRegistrations(Programs[0].ProgramId);
            }
                   
            if(Programs.Any())
                ProgramId = Programs[0].ProgramId;
        }

    public void OnPost()
    {
      RegistrationManager mgr = new RegistrationManager();
      Registrations = mgr.GetRegistrations(ProgramId);
    }

       

    public List<Enrollment> Registrations { get; set; } = new List<Enrollment>();

    public List<ProgramDetail> Programs { get; set; }

    [BindProperty]
    public Guid ProgramId { get; set; }

  }
}
