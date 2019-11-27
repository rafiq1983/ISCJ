using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using   Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ISCJ.Pages.School
{
    public class TeacherSubjectAssignment : BasePageModel
    {
        private List<Teacher> _teachers;
        private List<Subject> _subjects;
        private List<ProgramDetail> _programs;

        public void OnGet()
        {
            LoadTeacherSubjectMappings();
            LoadData();
            

        }

        private List<TeacherSubjectMapping> _teacherSubjectMappings = null;

        private void LoadTeacherSubjectMappings()
        {
                
                CourseManager mgr = new CourseManager();
                _teacherSubjectMappings = mgr.GetTeacherSubjectMappings(GetCallContext());
           
        }
        public List<TeacherSubjectMapping> TeacherSubjectMappings
        {
            get
            { 
                return _teacherSubjectMappings;

            }
        }

        public List<SelectListItem> Programs
        {
            get
            {
                return _programs.Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
            }
        }

        public List<SelectListItem> Subjects
        {
            get
            {
               
                    var output = _subjects.Select(x => new SelectListItem(x.SubjectName, x.SubjectId.ToString())).ToList();
                    return output;
            }
        }


        public List<SelectListItem> Teachers
        {
            get
            {
               
                var output = _teachers.Select(x => new SelectListItem(x.Contact.FirstName + " " + x.Contact.LastName, x.TeacherId.ToString())).ToList();
                return output;
            }
        }

        private void LoadData()
        {
            CourseManager mgr = new CourseManager();
            _teachers = mgr.GetTeachers(GetCallContext());
            _subjects = mgr.GetSubjects(GetCallContext());
            
            ProgramManager programManager = new ProgramManager();
            _programs = programManager.GetAllPrograms(GetCallContext());


        }

        public void OnPostSave()
        {
            PerformValidation();
            if (ModelState.IsValid)
            {
                CourseManager mgr = new CourseManager();
                mgr.AddTeacherSubjectMapping(GetCallContext(), ProgramId.Value, SubjectId.Value, TeacherId.Value);


            }

            LoadTeacherSubjectMappings();
            LoadData();
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

            var subjects = mgr.GetTeacherSubjectMappings(GetCallContext());
            if (subjects.Count(x => x.SubjectId == SubjectId && x.ProgramId == ProgramId && x.TeacherId == TeacherId) >
                0)
            {
                ModelState.AddModelError<TeacherSubjectAssignment>(y=>y.ProgramId, $"The teacher is already assigned to this subject in this program.");
                return;
            }

            if(subjects.Count(x=>x.SubjectId == SubjectId && x.ProgramId == ProgramId)>0)
                ModelState.AddModelError<TeacherSubjectAssignment>(y => y.ProgramId, $"The Subject is already assigned to another teacher in this program.");

        }

        [Required]
        [BindProperty] public Guid? TeacherId { get; set; }

        [Required]
        [BindProperty] public Guid? ProgramId { get; set; }
        [Required]
        [BindProperty] public Guid? SubjectId { get; set; }


       
    }
}