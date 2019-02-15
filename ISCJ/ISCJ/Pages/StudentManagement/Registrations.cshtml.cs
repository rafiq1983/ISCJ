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
        public void OnGet()
        {
            RegistrationManager mgr = new RegistrationManager();
      Registrations = mgr.GetRegistrations();
      
        }

    public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
