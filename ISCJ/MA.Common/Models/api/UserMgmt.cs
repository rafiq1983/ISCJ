using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    public class CreateUserInput
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }

        public ContactApi ContactInformation { get; set; }

    }

    public class CreateUserOutput
    {
        public Guid UserId { get; set; }
    }
}
