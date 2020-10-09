using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            UserManager mgr = new UserManager();
            var answers = new List<SecurityQuestionAnswer>()
            {
                new SecurityQuestionAnswer() {QuestionId =Guid.Empty, Answer = Answer}
            };

            var output = mgr.ResetPassword(new ResetPasswordInput()
            {
                EmailId = UserEmail,
                NewPassword = NewPassword,
                SecurityQuestionAnswers = answers
            });

            Message = output.Reason;

            if (output.Success)
            {
                Response.Redirect("/login");
            }

        }

        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public Guid QuestionId { get; set; }
        [BindProperty] public string UserEmail { get; set; }
        [BindProperty] public string NewPassword { get; set; }
        [BindProperty] public string Question { get; set; }
        [BindProperty] public string Answer { get; set; }
    }
}