using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;

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
        public int StudentNumber { get; set; }

        [ForeignKey("StudentContactId")]
        public List<Enrollment> Enrollments { get; set; }

        public Contact StudentContact { get; set; }
    }

    public class StudentSubject : BaseEntity
    {
        public Guid RecordId { get; set; }
        public Guid StudentId { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid TenantId { get; set; }

        public Subject Subject { get; set; }

    }
}
