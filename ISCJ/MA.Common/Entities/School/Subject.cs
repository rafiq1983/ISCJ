using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Registration;

namespace MA.Common.Entities.School
{
    public class Subject : BaseEntity
    {
        public Guid SubjectId { get; set; }

        public string SubjectDescription { get; set; }

        public string SubjectName{ get; set; }

        public Guid TenantId { get; set; }

        
    }

    public class SubjectMapping : BaseEntity
    {
        
        public Guid SubjectId { get; set; }
        public Guid ProgramId { get; set; }
        public string IslamicSchoolGradeId { get; set; }
        public Guid TenantId { get; set; }
     
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [ForeignKey("ProgramId")] public ProgramDetail Program { get; set; }
    }


    public class TeacherSubjectMapping : BaseEntity
    {
        public Guid RecordId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ProgramId { get; set; }
        public string TeacherId { get; set; }
        public Guid TenantId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [ForeignKey("ProgramId")] public ProgramDetail Program { get; set; }
    }


    public class Teacher : BaseEntity
    {

        public Guid TeacherId { get; set; }
        public Guid ContactId { get; set; }
        public Guid TenantId { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

        [ForeignKey("TeacherId")]
        public TeacherSubjectMapping TeacherSubjects { get; set; }

    }
}
