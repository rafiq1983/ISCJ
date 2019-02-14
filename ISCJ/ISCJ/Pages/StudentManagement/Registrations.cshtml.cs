using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
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

    public List<RegistrationDetail> Registrations { get; set; } = new List<RegistrationDetail>();
    }
}
