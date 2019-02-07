using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.StudentManagement
{
    public class RegistrationsModel : PageModel
    {
        public void OnGet()
        {

        }

    public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
