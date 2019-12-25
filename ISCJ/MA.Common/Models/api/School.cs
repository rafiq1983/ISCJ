using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
   public class AssignTeacherToClassInput
   {
       public Guid ClassId { get; set; }
       public Guid TeacherId { get; set; }
   }

   public class AddStudentClassNotes
   {
       public Guid StudentId { get; set; }
       public Guid ClassId { get; set; }
       public string Note { get; set; }
   }

   public class MarkStudentPresence { 
       public string ClassSessionId { get; set; }
       public bool IsPresent { get; set; }
   }

  
}
