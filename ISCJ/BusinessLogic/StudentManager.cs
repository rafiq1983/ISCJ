using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Models.api;

namespace BusinessLogic
{
  public class StudentManager
  {
    public string AddStudentGrade(string courseId, string gradeId)
    {
      return "";

    }

    public GetStudentListOutput GetStudentList(Guid programId)
    {
        var output = new GetStudentListOutput();
        output.Students = new List<StudentBasicInfo>();
        output.Students.Add(new StudentBasicInfo(){ClassGrade = "1", DOB = DateTime.MaxValue, FirstName = "Test First", LastName = "Test Last"});
        return output;
    }

    public GetStudentDetail GetStudentDetail(Guid studentId)
    {
        var output = new GetStudentDetail();

        output.StudentBasicInfo = new StudentDetail()
        {
             BasicInfo = new StudentBasicInfo()
                 { ClassGrade = "1", DOB = DateTime.MaxValue, FirstName = "Test First", LastName = "Test Last" }
        };

        return output;
    }

    }
}
