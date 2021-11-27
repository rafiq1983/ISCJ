﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;
using MA.Common.Entities.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ISCJ.Pages
{
    public class RecordStudentGrades : BasePageModel
    {
        public void OnGet()
        {
            LoadStudentSubjects();
            LoadSubjects();

            LoadPrograms();
            StudentMarks = StudentSubjects.Select(x => new StudentManagement.StudentMarks() { }).ToList();
        }

        public void OnPost()
        {
            LoadStudentSubjects();
            LoadSubjects();
            if (StudentMarks.Count == 0)//if data is posted by client, don't override it.
                StudentMarks = StudentSubjects.Select(x => new StudentManagement.StudentMarks() { }).ToList();
            LoadPrograms();
        }

        private void LoadStudentSubjects()
        {
            StudentManager mgr = new StudentManager();
            StudentSubjects = mgr.GetStudentSubjects(GetCallContext(), StudentId, ProgramId);
        }

        private void LoadSubjects()
        {
           CourseManager mgr = new CourseManager();
            Subjects = mgr.GetSubjects(GetCallContext())
                .Select(x => new SelectListItem(x.SubjectName, x.SubjectId.ToString())).ToList();
        }

        private void LoadPrograms()
        {
            ProgramManager mgr = new ProgramManager();
            Programs =
                mgr.GetAllPrograms(GetCallContext()).Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
        }

        public List<SelectListItem> Programs { get; private set; }
        public List<StudentSubject> StudentSubjects { get; private set; }

        [BindProperty] public List<StudentManagement.StudentMarks> StudentMarks { get; set; }

        public List<SelectListItem> Subjects { get; set; }
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