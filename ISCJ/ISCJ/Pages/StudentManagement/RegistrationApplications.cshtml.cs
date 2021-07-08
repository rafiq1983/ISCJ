using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using MA.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{

    [Authorize(AuthenticationSchemes = "Cookies,Captcha")] //if captcha is present, it will captcha authentication enabled.
    public class RegistrationApplicationsModel : BasePageModel
    {
    ProgramManager programMgr;
    public RegistrationApplicationsModel()
    {
      programMgr = new ProgramManager();

     }
        
        public void OnGet()
        {
            var user = HttpContext.User;
            var identities = user.Identities.ToList();
            var claims = user.Claims.ToList();


            Programs = programMgr.GetAllPrograms(GetCallContext());


            RegistrationManager mgr = new RegistrationManager();
            Registrations = mgr.GetAllApplications(GetCallContext(), Programs[0].ProgramId);
            if(Programs.Any())
                ProgramId = Programs[0].ProgramId;
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
