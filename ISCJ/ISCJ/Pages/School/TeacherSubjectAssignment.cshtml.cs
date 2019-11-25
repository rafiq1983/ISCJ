using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.School
{
    public class TeacherSubjectAssignment : BasePageModel
    {
        public void OnGet()
        {

        }

        private List<TeacherSubjectMapping> _teacherSubjectMappings = null;
        public List<TeacherSubjectMapping> TeacherSubjectMappings
        {
            get
            {
               // if (_subjectMappings == null)
                {
                    CourseManager mgr = new CourseManager();
                    _teacherSubjectMappings = mgr.GetTeacherSubjectMappings(GetCallContext());
                }

                return _teacherSubjectMappings;

            }
        }

        public List<SelectListItem> Programs
        {
            get
            {
                ProgramManager mgr = new ProgramManager();
                return mgr.GetAllPrograms(GetCallContext())
                    .Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
            }
        }

        public List<SelectListItem> Subjects
        {
            get
            {
                CourseManager mgr = new CourseManager();
                var subjects = mgr.GetSubjects(GetCallContext());

                    var output = subjects.Select(x => new SelectListItem(x.SubjectName, x.SubjectId.ToString())).ToList();
                    return output;
            }
        }


        public List<SelectListItem> Teachers
        {
            get
            {
                CourseManager mgr = new CourseManager();
                var teachers = mgr.GetTeachers(GetCallContext());

                var output = teachers.Select(x => new SelectListItem(x.Contact.FirstName + " " + x.Contact.LastName, x.TeacherId.ToString())).ToList();
                return output;
            }
        }


        public void OnPostSave()
        {
            PerformValidation();
            if (ModelState.IsValid)
            {
                CourseManager mgr = new CourseManager();
                mgr.AddTeacherSubjectMapping(GetCallContext(), ProgramId.Value, SubjectId.Value, TeacherId.Value);
            }
        }

        public void OnPosReset()
        {
            Reset();

        }
        public void Reset()
        {
            ModelState.Clear();
            TeacherId = null;
            ProgramId = null;
            SubjectId = null;
        }


        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/main");
        }



        private void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;
            CourseManager mgr = new CourseManager();
            var subject = mgr.GetSubjectMappings(GetCallContext(), SubjectId.Value);
            if(subject!=null)
                ModelState.AddModelError("SubjectId", $"This subject {SubjectId.Value} already has the mapping.");

        }

        [Required]
        [BindProperty] public Guid? TeacherId { get; set; }

        [Required]
        [BindProperty] public Guid? ProgramId { get; set; }
        [Required]
        [BindProperty] public Guid? SubjectId { get; set; }
        [Required]
        [BindProperty] public string IslamicSchoolGradeId { get; set; }


       
    }
}