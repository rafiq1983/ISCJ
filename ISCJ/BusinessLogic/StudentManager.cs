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

    public List<StudentSubject> GetStudentSubjects(CallContext context, Guid studentId, Guid programId)
    {
        using (Database db = new Database())
        {
            
                var output = db.StudentSubjects.Where(x => x.StudentId == studentId && x.TenantId == context.TenantId && x.ProgramId == programId)
                    .Include(x => x.Subject).ToList();
                return output;
            
        }
    }

    public Student GetStudent(CallContext context, Guid studentId)
    {
        using (Database db = new Database())
        {

            var output = db.Students.Where(x => x.StudentId == studentId && x.TenantId == context.TenantId).Include(x=>x.Enrollments)
                .SingleOrDefault();
            return output;

        }
    }


        public void AddStudent(CallContext context, Student student)
    {
        using (Database db = new Database())
        {

            db.Students.Add(student);
            db.SaveChanges();
        }
    }

    public Guid AddStudentSubject(CallContext context, Guid studentId, Guid enrollmentId, Guid subjectId, Guid progaramId)
    {
        using (Database db = new Database())
        {
            var studentSubject = new StudentSubject()
            {
                SubjectId = subjectId,
                RecordId = Guid.NewGuid(),
                ProgramId = progaramId,
                CreateUser = context.UserLoginName,
                CreateDate = DateTime.UtcNow,
                EnrollmentId = enrollmentId,
                TenantId = context.TenantId.Value,
                StudentId = studentId
            };

                db.StudentSubjects.Add(studentSubject);

            db.SaveChanges();

            return studentSubject.RecordId;
        }
    }

    public void AddMetricsToStudentSubject(CallContext context, Guid studentSubjectId, List<Guid> metricIds)
    {
        using (Database db = new Database())
        {
            foreach (Guid metricId in metricIds)
            {
                MetricValue mv = new MetricValue();
                mv.MetricValueId = Guid.NewGuid();
                mv.MetricId = metricId;
                mv.Data = string.Empty;
                mv.MetricPointerId = studentSubjectId;
                mv.MetricPointerType = "StudentSubject";
                mv.CreateUser = context.UserLoginName;
                mv.CreateDate = DateTime.UtcNow;
                mv.TenantId = context.TenantId.Value;


                db.MetricValues.Add(mv);
            }

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
