using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.School
{
    public class Subject : BaseEntity
    {
        public Guid SubjectId { get; set; }

        public string SubjectDescription { get; set; }

        public string SubjectLongDesc { get; set; }
    }

    public class SubjectMapping : BaseEntity
    {
        
        public Guid SubjectId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid IslamicSchoolGradeId { get; set; }
    }
}
