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

      Programs = programMgr.GetPrograms();
    }
        public void OnGet()
        {
            RegistrationManager mgr = new RegistrationManager();
            Registrations = mgr.GetRegistrations(Guid.Parse(Programs[0].ProgramId));
      ProgramId = Guid.Parse(Programs[0].ProgramId);
        }

    public void OnPost()
    {
      RegistrationManager mgr = new RegistrationManager();
      Registrations = mgr.GetRegistrations(ProgramId);
    }

       

    public List<Registration> Registrations { get; set; } = new List<Registration>();

    public List<ProgramDetail> Programs { get; set; }

    [BindProperty]
    public Guid ProgramId { get; set; }

  }
}
