﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    CreateUser = context.UserId,
                    CreateDate = DateTime.UtcNow,
                    SubjectId = Guid.NewGuid()
                };

                db.Subjects.Add(subject);
                db.SaveChanges();

                return subject.SubjectId;
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
                mapping.CreateUser = context.UserId;
                db.SubjectMappings.Add(mapping);
                db.SaveChanges();
                return true;
            }
        }

        public List<Subject> GetSubjects(CallContext context)
        {
            using (var db = new Database())
            {
                return db.Subjects.Where(x => x.TenantId == context.TenantId).ToList();
            }

        }

        public Subject GetSubjectByName(CallContext context, string subjectName)
        {
            using (var db = new Database())
            {
                return db.Subjects.SingleOrDefault(x => x.TenantId == context.TenantId && x.SubjectName == subjectName);
            }

        }

        public SubjectMapping GetSubjectMappings(CallContext context, Guid subjectId)
        {
            using (var db = new Database())
            {
                return db.SubjectMappings.Include(x => x.Program).Include(x => x.Subject).SingleOrDefault(x => x.TenantId == context.TenantId && x.SubjectId == subjectId);
            }

        }

        public List<SubjectMapping> GetAllSubjectMappings(CallContext context)
        {
            using (var db = new Database())
            {
                return db.SubjectMappings.Where(x => x.TenantId == context.TenantId).Include(x=>x.Program).Include(y=>y.Subject).ToList();
            }

        }
    }
}
