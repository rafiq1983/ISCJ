﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class SubjectAssignmentRulesModel : BasePageModel
    {

        private List<SubjectMapping> _subjectMappings = null;

        public void OnGet()
        {

            LoadSubjectMappings();

        }

        private void LoadSubjectMappings()
        {
            CourseManager mgr = new CourseManager();
            _subjectMappings = mgr.GetSubjectGradeMappings(GetCallContext());
        }

        public List<SubjectMapping> SubjectMappings
        {
            get
            {
              
                return _subjectMappings;

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

        public IEnumerable<SelectListItem> IslamicSchoolGradeList
        {
            get
            {
                return ListService.GetIslamicSchoolGradesList().Select(x => new SelectListItem(x.GradeName, x.GradeId));
            }
        }

        public void OnPost()
        {
            PerformValidation();
            if (ModelState.IsValid)
            {
                CourseManager mgr = new CourseManager();
                mgr.AddSubjectMapping(GetCallContext(), ProgramId.Value, SubjectId.Value, IslamicSchoolGradeId);
            }

            LoadSubjectMappings();
            

        }

        private void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;
            CourseManager mgr = new CourseManager();
            var subject = mgr.GetSubjectMappings(GetCallContext(), SubjectId.Value, ProgramId.Value);
            if(subject!=null)
                ModelState.AddModelError("SubjectId", $"This subject {subject.Subject.SubjectName} already has the mapping.");

        }

        [Required]
        [BindProperty] public Guid? ProgramId { get; set; }
        [Required]
        [BindProperty] public Guid? SubjectId { get; set; }
        [Required]
        [BindProperty] public string IslamicSchoolGradeId { get; set; }


       
    }
}