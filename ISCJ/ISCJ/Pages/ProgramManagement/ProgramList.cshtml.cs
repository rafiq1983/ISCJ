using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ISCJ.Pages.ProgramManagement
{
  public class ProgramListModel : PageModel
  {
    ProgramManager mgr = new ProgramManager();

    public void OnGet()
    {
      ProgramDetails = mgr.GetPrograms();
    }


    public List<ProgramDetail> ProgramDetails
    {
      get;
      private set;
    }
  }
}
