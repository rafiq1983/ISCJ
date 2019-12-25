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
                if (EditSubjectId == Guid.Empty)
                {

                    courseManager.AddSubject(GetCallContext(), SubjectName, SubjectDescription);
                    
                }
                else
                {
                    courseManager.UpdateSubject(GetCallContext(), EditSubjectId, SubjectName, SubjectDescription);
                  
                }

                SubjectName = string.Empty;
                SubjectDescription = string.Empty;

                ModelState.Clear(); //this is necessary so controls can rebind to the Model Values instead of ModelState.
            }

            LoadSubjects();

        }

        [BindProperty]
        public Guid EditSubjectId { get; set; }

        public void OnPostUpdate([FromForm]Guid btnUpdateSubject)
        {
            CourseManager courseManager = new CourseManager();
            
                var subject = courseManager.GetSubjectById(GetCallContext(), btnUpdateSubject);
                SubjectName = subject.SubjectName;
                SubjectDescription = subject.SubjectDescription;
                EditSubjectId = subject.SubjectId;
                ModelState.Clear();//this is necessary so controls can rebind to the Model instead of ModelState.

            LoadSubjects();

        }

        public void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;
            CourseManager courseManager = new CourseManager();

            var subject = courseManager.GetSubjectByName(GetCallContext(), SubjectName);

            if (subject != null && subject.SubjectId!=EditSubjectId)
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
        [Required()]
        public string SubjectDescription { get; set; }
    }
}