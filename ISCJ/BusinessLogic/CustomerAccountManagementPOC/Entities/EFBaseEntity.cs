using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BusinessLogic.CustomerAccountManagementPOC.Entities
{
    public class EFBaseEntity
    {  
        public string CreateUser { get; private set; }//iftikhar: entity framework is still able to set it.
        [ExcludeFromCodeCoverage]
        public DateTime CreateDate { get; private set; }
        public string UpdateUser { get; private set; }
        [ExcludeFromCodeCoverage]
        public DateTime? UpdateDate { get; protected set; }

     
    }
}
