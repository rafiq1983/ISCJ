using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MA.Common.Entities.Registration;
namespace ISCJ.Pages.ProgramManagement
{
  public class ProgramListModel : PageModel
  {
    ProgramManager mgr = new ProgramManager();

    public void OnGet()
    {
            ProgramDetails = mgr.GetAllPrograms(new MA.Core.CallContext("Iftikhar", "23434", "234234234", Guid.Empty));
    }


    public List<ProgramDetail> ProgramDetails
    {
      get;
      private set;
    }
  }
}
