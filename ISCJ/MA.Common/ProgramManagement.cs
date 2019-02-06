using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common
{
  public class ProgramDetail
  {
    public string ProgramId { get; set; }
    public string ProgramName { get; set; }

    public string ProgramDescription { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public List<DayOfWeek> Days { get; set; }

    public DateTime Time { get; set; }
  }
}
