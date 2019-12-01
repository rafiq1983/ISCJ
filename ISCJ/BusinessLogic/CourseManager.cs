using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Entities.Registration;
using MA.Common.Entities.School;
using MA.Core;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class CourseManager
    {
        public Guid AddSubject(CallContext context, string subjectName, string subjectDescription)
        {
            using (var db = new Database())
            {
                var subject = new Subject()
                {
                    SubjectName = subjectName,
                    SubjectDescription = subjectDescription,
                    CreateUser = context.UserLoginName,
                    CreateDate = DateTime.UtcNow,
                    TenantId = context.TenantId.Value,
                    SubjectId = Guid.NewGuid()
                };

                db.Subjects.Add(subject);
                db.SaveChanges();

                return subject.SubjectId;
            }
        }

        public Guid AddTeacher(CallContext context, Guid contactId)
        {
            using (var db = new Database())
            {
                var teacher = new Teacher()
                {
                    TeacherId = Guid.NewGuid(),
                    ContactId = contactId,
                    CreateUser = context.UserLoginName,
                    CreateDate = DateTime.UtcNow,
                    TenantId = context.TenantId.Value
                    
                };

                db.Teachers.Add(teacher);
                db.SaveChanges();

                return teacher.TeacherId;
            }
        }


        public bool AddSubjectMapping(CallContext context, Guid programId, Guid subjectId, string islamicSchoolGradeId)
        {
            using (var db = new Database())
            {
                SubjectMapping mapping = new SubjectMapping();
                mapping.ProgramId = programId;
                mapping.SubjectId = subjectId;
                mapping.IslamicSchoolGradeId = islamicSchoolGradeId;
                mapping.CreateUser = context.UserLoginName;
                mapping.TenantId = context.TenantId.Value;
                db.SubjectMappings.Add(mapping);
                
                db.SaveChanges();
                return true;
            }
        }

        public bool AddTeacherSubjectMapping(CallContext context, Guid programId, Guid subjectId, Guid teacherId)
        {
            using (var db = new Database())
            {
               TeacherSubjectMapping mapping = new TeacherSubjectMapping();
                mapping.ProgramId = programId;
                mapping.SubjectId = subjectId;
                mapping.TenantId = context.TenantId.Value;
                mapping.CreateUser = context.UserLoginName;
                mapping.CreateDate = DateTime.UtcNow;
                mapping.TenantId = context.TenantId.Value;
                mapping.TeacherId = teacherId;
                db.TeacherSubjectMappings.Add(mapping);

                db.SaveChanges();
                return true;
            }
        }



        public List<Room> GetRooms(CallContext context)
        {
            using (var db = new Database())
            {
                return db.Rooms.Where(x => x.TenantId == context.TenantId).ToList();
            }

        }

        public int AddRoom(CallContext context, string roomName, string roomDescription)
        {
            using (var db = new Database())
            {
                Room room = new Room();
                room.TenantId = context.TenantId.Value;
                room.RoomName = roomName;
                room.RoomDescription = string.IsNullOrEmpty(roomDescription) ? string.Empty : roomDescription;
                room.CreateDate = DateTime.UtcNow;
                room.CreateUser = context.UserLoginName;

                db.Rooms.Add(room);
                
                db.SaveChanges();

                return room.RoomId; //Identity column auto populates.
            }

        }

        public List<Subject> GetSubjects(CallContext context)
        {
            using (var db = new Database())
            {
                return db.Subjects.Where(x => x.TenantId == context.TenantId).ToList();
            }

        }

       
        public List<Teacher> GetTeachers(CallContext context)
        {
            using (var db = new Database())
            {
                return db.Teachers.Where(x => x.TenantId == context.TenantId).Include(x=>x.Contact).ToList();
            }

        }

        public Teacher GetTeacherByTeacherId(CallContext context, Guid teacherId)
        {
            using (var db = new Database())
            {
                return db.Teachers.SingleOrDefault(x => x.TenantId == context.TenantId && x.TeacherId == teacherId);
            }

        }

        public Teacher GetTeacherByContactId(CallContext context, Guid contactId)
        {
            using (var db = new Database())
            {
                return db.Teachers.SingleOrDefault(x => x.TenantId == context.TenantId && x.ContactId == contactId);
            }

        }


        public Subject GetSubjectByName(CallContext context, string subjectName)
        {
            using (var db = new Database())
            {
                return db.Subjects.SingleOrDefault(x => x.TenantId == context.TenantId && x.SubjectName == subjectName);
            }

        }

        public List<SubjectMapping> GetSubjectByProgramAndIslamicGradeId(CallContext context, Guid programId,
            string islamicGradeId)
        {
            using (var db = new Database())
            {
                return db.SubjectMappings.Where(x =>
                        x.TenantId == context.TenantId && x.IslamicSchoolGradeId == islamicGradeId &&
                        x.ProgramId == programId)
                    .ToList();
            }
        }

        public SubjectMapping GetSubjectMappings(CallContext context, Guid subjectId)
        {
            using (var db = new Database())
            {
                return db.SubjectMappings.Include(x => x.Program).Include(x => x.Subject).SingleOrDefault(x => x.TenantId == context.TenantId && x.SubjectId == subjectId);
            }

        }

        public List<SubjectMapping> GetSubjectGradeMappings(CallContext context)
        {
            using (var db = new Database())
            {
               var output = db.SubjectMappings.Where(x => x.TenantId == context.TenantId).Include(x=>x.Program).Include(x=>x.Subject).ToList();
               return output;
            }

        }

        public List<TeacherSubjectMapping> GetTeacherSubjectMappings(CallContext context)
        {
            using (var db = new Database())
            {
                var output = db.TeacherSubjectMappings.Where(x => x.TenantId == context.TenantId).Include(x => x.Program).Include(x => x.Subject).Include(x=>x.Teacher).ThenInclude(x=>x.Contact).ToList();
                return output;
            }

        }
    }
}
