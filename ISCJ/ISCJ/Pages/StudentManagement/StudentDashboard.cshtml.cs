﻿using System;
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
        }

        public void OnPost()
        {
            LoadStudentSubjects();
            LoadStudents();
            LoadPrograms();
        }

        private void LoadStudentSubjects()
        {
            StudentManager mgr = new StudentManager();
           StudentSubjects = mgr.GetStudentSubjects(GetCallContext(), StudentId, ProgramId);
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
}