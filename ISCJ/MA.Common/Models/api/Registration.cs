using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    public class CreateProgramInput
    {
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
    }

    public class CreateProgramOutput
    {
        public Guid ProgramId { get; set; }
    }

    public class AddClassToProgramInput
    {
        public Guid ProgramId { get; set; }
        public List<Guid> ClassIds { get; set; }
    }

    public class AddClassToProgramOutput
    {
        public bool Success { get; set; }
    }

    public class AddAClassInput
    {
        public string ClassDescription { get; set; }
        public Guid ProgramId { get; set; }
        public string ClassGrade { get; set; }
    }

    public class AddAClassOutput
    {
        public string ClassId { get; set; }
    }

    public class CreateRegistrationApplicationInput
    {
        public Guid FatherContactId { get; set; }
        public Guid MotherContactId { get; set; }
        public List<StudentRegistrationApplication> StudentRegistration;
        public Guid ProgramId { get; set; }

        public List<RegistrationApplicationBilling> BillProducts { get; set; }
    }

    public class RegistrationApplicationBilling
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
    public class StudentRegistrationApplication
    {
        public Guid studentContactId { get; set; }
        public string IslamicSchoolGrade { get; set; }
        public string PublicSchoolGrade { get; set; }
    }

    public class CreateRegistrationApplicationOutput
    {
        public Guid ApplicationId { get; set; }
    }
}
