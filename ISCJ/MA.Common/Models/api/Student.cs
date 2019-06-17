using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
    public class GetStudentListOutput
    {
        public List<StudentBasicInfo> Students { get; set; }
    }

    public class StudentBasicInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public bool IsEnrolled { get; set; }
        public string ClassGrade { get; set; }
    }

}
