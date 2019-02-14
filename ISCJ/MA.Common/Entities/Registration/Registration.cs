using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities.Registration
{
  public class Registration
  {
    public Guid RegistrationId { get; set; }

    public Guid FatherId { get; set; }

    public Guid MotherId { get; set; }

    public Guid ProgramId { get; set; }

    public string IslamicSchoolGradeId { get; set; }

    public string PublicSchoolGradeId { get; set; }

    public Guid StudentId { get; set; }

  }


}
