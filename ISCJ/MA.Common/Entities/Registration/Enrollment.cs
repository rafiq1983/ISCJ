using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MA.Common.Entities.Contacts;
using MA.Common.Entities.Invoices;

namespace MA.Common.Entities.Registration
{
    public class Room : BaseEntity
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public Guid TenantId { get; set; }

    }
  public class Enrollment:BaseEntity
  {
    public Guid EnrollmentId { get; set; }
   
        public Guid RegistrationApplicationId { get; set; }
        public Guid FatherId { get; set; } //isa:may not need these.
        public Guid MotherId { get; set; }

    public Guid ProgramId { get; set; }

    public string IslamicSchoolGradeId { get; set; }

    public Guid TenantId { get; set; }

    public string PublicSchoolGradeId { get; set; }

    public Guid StudentContactId { get; set; }

    [ForeignKey("FatherId")] //can be done with fluent api as well.
    public Contact FatherContactInfo { get; set; }

    [ForeignKey("MotherId")]
    public Contact MotherContactInfo { get; set; }

    [ForeignKey("StudentContactId")]
    public Contact StudentContactInfo { get; set; }//needs to map to contact table.

    [ForeignKey("RegistrationApplicationId")]
    public RegistrationApplication RegistrationApplication{ get; set; }


    }

    public class RegistrationApplication:BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public Guid ProgramId { get; set; }

        public Guid? MembershipId { get; set; }
        public Guid FatherContactId { get; set; } //isa:may not need these.

        public Guid MotherContactId { get; set; }

        [ForeignKey("RegistrationApplicationId")]
        public List<Enrollment> Enrollments { get; set; }//needs to map to contact table.*/

        [ForeignKey("FatherContactId")]
        public Contact FatherContactInfo { get; set; }
        [ForeignKey("MotherContactId")]
        public Contact MotherContactInfo { get; set; }

       // [ForeignKey("ReferenceId")]
        //public List<Invoice> Invoices { get; set; }

    }


}
