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
    public class InstructorsListModel : BasePageModel
    {
        public void OnGet()
        {

        }

   

        public List<Teacher> Teachers
        {
            get
            {
                CourseManager courseManager = new CourseManager();
                return courseManager.GetTeachers(GetCallContext());
            }
        }

        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

       
    }
}