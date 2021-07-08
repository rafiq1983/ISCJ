using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MA.Common
{
    public class SaveUserSecurityQuestionsInput
    {
        public List<SecurityQuestionAnswer> SecurityQuestionAnswers { get; set; }
    }

    public class SecurityQuestionAnswer
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }

    public class GetSecurityQuestionsOutput
    {
        public List<SecurityQuestionAnswer> SecurityQuestions { get; set; }
    }


    public class SecurityQuestion
    {
        public Guid QuestionId
        {
            get; set;

        }

        public string QuestionText { get; set; }
    }

    public class SaveUserSecurityOutput
    {

    }
    public class VerifyLoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordOutput
    {
        public bool Success { get; set; }
        public string Reason { get; set; }
    }

    public class ResetPasswordInput
    {
        public string EmailId { get; set; }
        public string NewPassword { get; set; }
        public List<SecurityQuestionAnswer> SecurityQuestionAnswers { get; set; }
    }

    public class VerifyLoginOutput
    {
        public bool Success { get; set; }
        public bool RequiresPasswordChange { get; set; }
    }

    public class NewUserSignupInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserSignupOutput
    {
        public bool Success;
        public string ErrorMessage { get; set; }

    }


    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AuthenticationSource AuthenticationSource { get; set; }
    }

    public enum AuthenticationSource
    {
        Self, Gmail
    }

    public class AddUserToTenantOutput
    {
        public bool Success { get; set; }

    }

    public class AddUserToTenantInput
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string RoleCd { get; set; }
    }

}
