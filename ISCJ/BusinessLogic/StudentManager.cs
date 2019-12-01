using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Entities.Student;
using MA.Common.Models.api;
using MA.Core;

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

    public void AddStudent(CallContext context, Student student)
    {
        using (Database db = new Database())
        {
            db.Students.Add(student);
            db.SaveChanges();
        }
    }

    public void AddStudentSubject(CallContext context, Guid studentId, Guid enrollmentId, Guid subjectId, Guid progaramId)
    {
        using (Database db = new Database())
        {
                db.StudentSubjects.Add(new StudentSubject()
                {
                    SubjectId = subjectId,
                    RecordId = Guid.NewGuid(),
                    ProgramId = progaramId, 
                    CreateUser = context.UserLoginName,
                    CreateDate = DateTime.UtcNow,
                    EnrollmentId = enrollmentId,
                    TenantId = context.TenantId.Value,
                    StudentId = studentId
                });

            db.SaveChanges();
        }
    }

        public Student GetStudentByContactId(CallContext context, Guid contactId)
    {
        using (Database db = new Database())
        {
            return db.Students.SingleOrDefault(x => x.ContactId == contactId);
        }
    }

    }
}
