using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.School
{
    public class AddInstructorModel : BasePageModel
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
                courseManager.AddTeacher(GetCallContext(), TeacherContactId.Value);
               Reset();
            }

        }

        public void Reset()
        {
            TeacherContactId = null;
            TeacherName = null;
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

        [BindProperty]
        [Required]
        public Guid? TeacherContactId { get; set; }

        [BindProperty]
       
        public string TeacherName { get; set; }
    }
}