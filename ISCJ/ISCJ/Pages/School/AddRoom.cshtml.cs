﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.School
{
    public class AddRoomModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostSave()
        {
            CourseManager courseManager = new CourseManager();
            PerformValidation();

            if (ModelState.IsValid)
            {
                int roomId = courseManager.AddRoom(GetCallContext(), RoomName, RoomDescription);
                ModelState.Clear();
            }

        }

        public void PerformValidation()
        {
            if (!ModelState.IsValid)
                return;


        }


        private CallContext GetCallContext()
        {
            return new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty);
        }

        [BindProperty]
        [Required]
        public string RoomName { get; set; }

        [BindProperty] [Required] public string RoomDescription { get; set; }

    }
}