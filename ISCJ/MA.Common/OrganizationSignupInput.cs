using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common
{
   public class OrganizationSignupInput
    {
        public string OrganizationName { get; set; }
        public string SignupEmail { get; set; }
        public string Password { get; set; }
        public string DisplayTimeZone { get; set; } = "Eastern Standard Time";
    }

   public class OrganizationSignupOutput
   {
       public bool Success { get; set; }
       public SignupFailureReason FailureReason { get; set; }
    
   }

   public enum SignupFailureReason
   {
       OrganizationAlreadyExists, Other, LoginFailed
   }
}
