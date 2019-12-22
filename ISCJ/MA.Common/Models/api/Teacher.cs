using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    /// <summary>
    /// Add teacher row, if contact id is present.  Else add new a new contact row and then creates a new teacher using newly generated contactid.
    /// </summary>
   public class AddTeacherInput
    {
        public ContactApi TeacherInfo { get; set; }
        public Guid? ContactId { get; set; }
    }
    public class AddTeacherOutput
    {
        public Guid TeacherId { get; set; }
    }

    public class AssignTeacherToCourse
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
    }
}
