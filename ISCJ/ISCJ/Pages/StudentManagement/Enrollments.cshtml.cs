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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ISCJ.Pages.StudentManagement
{
    public class RegistrationsModel : PageModel
    {
    ProgramManager programMgr;

    private CallContext GetCallContext()
    {
        return new MA.Core.CallContext("Iftikhar", "23234", "asfasf", Guid.Empty);
    }
    public RegistrationsModel()
    {
      programMgr = new ProgramManager();

            Programs = programMgr.GetAllPrograms(GetCallContext());
    }
        public void OnGet()
        {
            RegistrationManager mgr = new RegistrationManager();
            Guid registrationAppId;
            bool validRegisAppId  = Guid.TryParse(Request.Query["regappid"], out registrationAppId);


            if (validRegisAppId)
            {
                var regisApp = mgr.GetApplication(GetCallContext(), registrationAppId);

                Enrollments = mgr.GetEnrollments(regisApp.ProgramId, registrationAppId);

                ProgramId = regisApp.ProgramId;
            }
            else
            {
                Enrollments = mgr.GetEnrollments(Programs[0].ProgramId);
            }
                   
            if(Programs.Any())
                ProgramId = Programs[0].ProgramId;
        }

    public void OnPost()
    {
      RegistrationManager mgr = new RegistrationManager();
      Enrollments = mgr.GetEnrollments(ProgramId);
    }

       

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public List<ProgramDetail> Programs { get; set; }

    [BindProperty]
    public Guid ProgramId { get; set; }

  }
}
