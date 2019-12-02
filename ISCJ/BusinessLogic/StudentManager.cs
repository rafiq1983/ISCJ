using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Entities.Registration;
using MA.Common.Entities.Student;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
  public class StudentManager
  {
    public string AddStudentGrade(string courseId, string gradeId)
    {
      return "";

    }

    public List<Student> GetStudentList(CallContext context, Guid? programId)
    {
        var output = new GetStudentListOutput();

        using (Database db = new Database())
        {
           var querable = db.Students.Include(x=>x.StudentContact).Where(x => x.TenantId == context.TenantId);

           if (programId != null && programId.Value !=Guid.Empty)
           {
               //TODO: Have to join with enrollments where programid = input program id.
              // querable = querable.Join(db.Enrollments).Where((Enrollment x) => x.ProgramId == programId.Value);
           }

           return querable.ToList();
        }
    }

    public List<StudentSubject> GetStudentSubjects(CallContext context, Guid studentId)
    {
        using (Database db = new Database())
        {
            return db.StudentSubjects.Where(x => x.StudentId == studentId && x.TenantId == context.TenantId).ToList();


        }
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
