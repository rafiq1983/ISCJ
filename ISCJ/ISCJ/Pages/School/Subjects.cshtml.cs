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

        }

        public void OnPost()
        {
            ;

        }

        

        public List<Subject> Subjects
        {
            get
            {
                CourseManager courseManager = new CourseManager();
                return courseManager.GetSubjects(GetCallContext());
            }
        }

        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

       
    }
}