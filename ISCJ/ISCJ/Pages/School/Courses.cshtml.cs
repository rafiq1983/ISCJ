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
    public class CoursesModel : PageModel
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
                courseManager.AddSubject(GetCallContext(), SubjectName, SubjectDescription);
                SubjectName = string.Empty;
                SubjectDescription = string.Empty;
                ModelState.Clear();

            }

        }

        public void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;
               CourseManager courseManager = new CourseManager();

               var subject = courseManager.GetSubjectByName(GetCallContext(), SubjectName);

               if (subject != null)
               {
                   ModelState.AddModelError("SubjectName", "Subject name already exists");
               }

           }

        public List<Subject> Subjects
        {
            get
            {
                CourseManager courseManager = new CourseManager();
                return courseManager.GetSubjects(GetCallContext());
            }
        }

        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

        [BindProperty]
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        [BindProperty]
        [MaxLength(500)]
        public string SubjectDescription { get; set; }
    }
}