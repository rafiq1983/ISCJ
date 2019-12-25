using MA.Common.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ.Pages.PageModels
{
    public class RegisterStudentViewModel
    {

        public string ProgramId{get;set;}      
        public Contact FatherInfo { get; set; }
        public Contact MotherInfo { get; set; }
        public List<Contact> Dependents { get; set; }



    }
}
