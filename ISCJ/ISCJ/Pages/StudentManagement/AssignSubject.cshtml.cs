using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.Pages
{
    public class AssignSubject : BasePageModel
    {
        private List<Subject> _allSubjects = null;

        public void OnGet(string entityType)
        {
            EntityType = entityType;
            LoadPageData();

        }



        private void LoadPageData()
        {
            CourseManager mgr = new CourseManager();
            _allSubjects = mgr.GetSubjects(GetCallContext());

            StudentManager studentMgr = new StudentManager();
            var student = studentMgr.GetStudentByContactId(GetCallContext(), EntityId);
            EntityValue =
                student.StudentContact.FirstName + " " +
                student.StudentContact.LastName; // mgr.GetEntityName(GetCallContext(), EntityId, EntityType);

            SelectedSubjects = _allSubjects.Select(x => new SelectedSubjectModel()
            {
                IsSelected = false,
                Subject = x
            }).ToList();

           var studentSubjects = studentMgr.GetStudentSubjects(GetCallContext(), EntityId, ProgramId);

           studentSubjects.ForEach(x=>SelectedSubjects.Single(y=>y.Subject.SubjectId == x.SubjectId).IsSelected = true);
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                StudentManager mgr = new StudentManager();

                foreach (var selected in SelectedSubjects.Where(x=>x.IsSelected))
                {
                    mgr.AddStudentSubject(GetCallContext(), EntityId, EnrollmentId, selected.Subject.SubjectId, ProgramId);
                }
               
            }

            LoadPageData();
        }

        public Guid EnrollmentId { get; set; }
        public Guid ProgramId { get; set; }

        private void ClearPage()
        {
                ModelState.Clear();
                EntityType = string.Empty;
                EntityId = Guid.Empty;

        }
        public void OnPostReset()
        {
           ClearPage();
        }

        public void OnPostCancel()
        {
            Response.Redirect("/main");
        }

     
        
        //supports get will allow population of Entity id from Url params.
        [BindProperty(SupportsGet=true)] [Required] public Guid EntityId { get; set; }

        [BindProperty(SupportsGet = true)] [Required] public string EntityType { get; set; }

       

        public string EntityFriendlyName
        {
            get { return EntityType; }
           
        }

        public string EntityValue
        {
            get;
            private set;

        }
        [BindProperty]
        public List<SelectedSubjectModel> SelectedSubjects { get; set; }
    }

    public class SelectedSubjectModel
    {
        public bool IsSelected { get; set; }
        public Subject Subject { get; set; }
    }
    
  
}