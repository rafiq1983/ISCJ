using MA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
  public class ProgramManager
  {
    static List<ProgramDetail> _programs;

    static ProgramManager()
    {
      _programs = new List<ProgramDetail>();

      _programs.Add(new ProgramDetail() { ProgramId = "333e070b-58b5-4f28-91b0-c20c56072859", ProgramName = "ISCJ Sunday School 2018-2019",
        StartDate = DateTime.Parse("9/1/2018"), EndDate = DateTime.Parse("6/1/2019"),

        ProgramDescription = @"<ul><li>Membership: $365 per year Membership can also be paid 3 installments as follows: <br/> <ul><li>165 at time of registration</li 
                                  <li>100 in December 2018</li><li>100 in March 2019</ul></li>
<li>Registration: Fee: $20 Per child. $25.00 after 09/2018</li></ul>"

    });
    
    }
    public List<ProgramDetail> GetPrograms()
    {
    return _programs;
    }

    public string AddProgram(ProgramDetail programDetail)
    {
      var program = _programs.SingleOrDefault(x => x.ProgramId == programDetail.ProgramId);

      if (program == null)
      {
        //new
        programDetail.ProgramId = Guid.NewGuid().ToString();
        _programs.Add(programDetail);
      }
      else
      {
        _programs[_programs.IndexOf(program)] = programDetail;
      }

      return programDetail.ProgramId;

    }
  }
}
