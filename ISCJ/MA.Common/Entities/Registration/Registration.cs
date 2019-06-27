using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MA.Common.Entities.Contacts;

namespace MA.Common.Entities.Registration
{
  public class Registration
  {
    public Guid RegistrationId { get; set; }
   
    public Guid FatherId { get; set; } //isa:may not need these.

    public Guid MotherId { get; set; }

    public Guid ProgramId { get; set; }

    public string IslamicSchoolGradeId { get; set; }

    public string PublicSchoolGradeId { get; set; }

    public Guid StudentId { get; set; }

    [ForeignKey("FatherId")] //can be done with fluent api as well.
    public Contact FatherContactInfo { get; set; }

    [ForeignKey("MotherId")]
    public Contact MotherContactInfo { get; set; }

    [ForeignKey("StudentId")]
    public Contact StudentContactInfo { get; set; }//needs to map to contact table.
    
  }


}
