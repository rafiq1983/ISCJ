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
    public class AddSubjectModel : BasePageModel
    {
        public void OnGet()
        {
            LoadSubjects();

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

            LoadSubjects();

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

        private void LoadSubjects()
        {
            CourseManager mgr = new CourseManager();

            Subjects = mgr.GetSubjects(GetCallContext());
        }


        public List<Subject> Subjects { get; private set; }



        [BindProperty]
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        [BindProperty]
        [MaxLength(500)]
        public string SubjectDescription { get; set; }
    }
}