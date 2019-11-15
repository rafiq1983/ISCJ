using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.School
{
    public class AddInstructorModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            CourseManager courseManager = new CourseManager();
            PerformValidation();

            if (ModelState.IsValid)
            {
                courseManager.AddTeacher(GetCallContext(), TeacherContactId);
            }

        }

        public void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;



            if (TeacherContactId == Guid.Empty)
            {
                ModelState.AddModelError("TeacherContactId", "Teacher must be selected.");
            }
        }


        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

        [BindProperty]
        [Required]
        public Guid TeacherContactId { get; set; }

        [BindProperty]
        public string TeacherName { get; set; }
    }
}