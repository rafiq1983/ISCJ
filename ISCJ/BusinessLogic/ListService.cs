using MA.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
  public class ListService
  {
    public static List<SchoolGrade> GetIslamicSchoolGradesList()
    {
      
      if (CacheService.GetData(CacheService.IslamicSchoolGradeListKey) == null)
      {
        List<SchoolGrade> grades = new List<SchoolGrade>()
        {
          new SchoolGrade(){ GradeId = "0", GradeName="Pre-K" },
          new SchoolGrade(){ GradeId = "1", GradeName="Kindergarten" },
           new SchoolGrade(){ GradeId = "2", GradeName="1st" },
           new SchoolGrade(){ GradeId = "3", GradeName="2nd" },
           new SchoolGrade(){ GradeId = "4", GradeName="3rd" },
           new SchoolGrade(){ GradeId = "5", GradeName="4th" },
           new SchoolGrade(){ GradeId = "6", GradeName="5th" },
           new SchoolGrade(){ GradeId = "7", GradeName="6th" },
           new SchoolGrade(){ GradeId = "8", GradeName="7th" },
           new SchoolGrade(){ GradeId = "9", GradeName="8th" },
           new SchoolGrade(){ GradeId = "10", GradeName="9th" },
           new SchoolGrade(){ GradeId = "11", GradeName="11th" },
           new SchoolGrade(){ GradeId = "12", GradeName="12th" },
        };
        CacheService.AddData(CacheService.IslamicSchoolGradeListKey, grades);
      }

      return CacheService.GetData(CacheService.IslamicSchoolGradeListKey) as List<SchoolGrade>;

    }

    public static List<SchoolGrade> GetPublicSchoolGradeList()
    {

      if (CacheService.GetData(CacheService.PublicSchoolGradeListKey) == null)
      {
        List<SchoolGrade> grades = new List<SchoolGrade>()
        {
          new SchoolGrade(){ GradeId = "0", GradeName="Pre-K" },
          new SchoolGrade(){ GradeId = "1", GradeName="Kindergarten" },
           new SchoolGrade(){ GradeId = "2", GradeName="1st" },
           new SchoolGrade(){ GradeId = "3", GradeName="2nd" },
           new SchoolGrade(){ GradeId = "4", GradeName="3rd" },
           new SchoolGrade(){ GradeId = "5", GradeName="4th" },
           new SchoolGrade(){ GradeId = "6", GradeName="5th" },
           new SchoolGrade(){ GradeId = "7", GradeName="6th" },
           new SchoolGrade(){ GradeId = "8", GradeName="7th" },
           new SchoolGrade(){ GradeId = "9", GradeName="8th" },
           new SchoolGrade(){ GradeId = "10", GradeName="9th" },
           new SchoolGrade(){ GradeId = "11", GradeName="11th" },
           new SchoolGrade(){ GradeId = "12", GradeName="12th" },
        };
        CacheService.AddData(CacheService.PublicSchoolGradeListKey, grades);
      }

      return CacheService.GetData(CacheService.PublicSchoolGradeListKey) as List<SchoolGrade>;
    }

    }
  }
