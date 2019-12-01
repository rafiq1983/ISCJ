using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Student
{
    public class Student:BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid ContactId { get; set; }
        public Guid FatherContactId { get; set; }
        public Guid MotherContactId { get; set; }
        public Guid EmergencyContactId { get; set; }
        public Guid TenantId { get; set; }
    }

    public class StudentSubject : BaseEntity
    {
        public Guid RecordId { get; set; }
        public Guid StudentId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid TenantId { get; set; }
    }
}
