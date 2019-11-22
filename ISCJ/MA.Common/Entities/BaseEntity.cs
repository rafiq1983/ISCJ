using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string CreateUser { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

    }
}
