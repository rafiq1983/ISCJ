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
    public class SubjectsListModel : BasePageModel
    {
        public void OnGet()
        {
            LoadSubjects();
        }

        public void OnPost()
        {
            ;

        }


        private void LoadSubjects()
        {
            CourseManager mgr = new CourseManager();

            Subjects = mgr.GetSubjects(GetCallContext());
        }
        

        public List<Subject> Subjects { get; private set; }

     
       
    }
}