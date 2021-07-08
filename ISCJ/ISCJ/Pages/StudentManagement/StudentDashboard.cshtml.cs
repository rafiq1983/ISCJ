using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using MA.Common.Entities.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages.StudentManagement
{
    public class StudentDashboardModel : BasePageModel
    {
     
        public void OnGet()
        {
            LoadStudentSubjects();
            LoadStudents();

            LoadPrograms();
            StudentMarks = StudentSubjects.Select(x=>new StudentMarks(){}).ToList();
        }

        public void OnPost()
        {
            LoadStudentSubjects();
            LoadStudents();
            if(StudentMarks.Count ==0)//if data is posted by client, don't override it.
            StudentMarks = StudentSubjects.Select(x => new StudentMarks() { }).ToList();
            LoadPrograms();
        }

        private void LoadStudentSubjects()
        {
            StudentManager mgr = new StudentManager();
           StudentSubjects = mgr.GetStudentSubjects(GetCallContext(), StudentId, ProgramId);
           SelectedStudent = mgr.GetStudent(GetCallContext(), StudentId);
        }

        public Student SelectedStudent
        {
            get;
            private set;
        }

        private void LoadStudents()
        {
            StudentManager mgr = new StudentManager();
            Students = mgr.GetStudentList(GetCallContext(), ProgramId)
                .Select(x => new SelectListItem(GetContactName(x.StudentContact), x.StudentId.ToString())).ToList();
        }

        private void LoadPrograms()
        {
            ProgramManager mgr = new ProgramManager();
            Programs =
                mgr.GetAllPrograms(GetCallContext()).Select(x=>new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
        }

        public List<SelectListItem> Programs { get; private set; }
        public List<StudentSubject> StudentSubjects { get; private set; }

        [BindProperty] public List<StudentMarks> StudentMarks { get; set; } 

        public List<SelectListItem> Students { get; set; }
        [BindProperty]
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        [BindProperty] public Guid ProgramId { get; set; }

        [Required]
        [BindProperty]
        public Guid SubjectId { get; set; }
    }

    public class StudentMarks

    {
        public Guid SubjectId { get; set; }
        public int Term1Marks { get; set; }

        public int Term2Marks { get; set; }

        public int Term3Marks { get; set; }

        public int Term4Marks { get; set; }
    }
}