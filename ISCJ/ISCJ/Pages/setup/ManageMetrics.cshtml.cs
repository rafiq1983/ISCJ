using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using MA.Common.Entities.Registration;
using Microsoft.AspNetCore.Mvc;

namespace ISCJ.Pages
{
    public class ManageMetricsModel : BasePageModel
    {
        public void OnGet()
        {

        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                ProgramManager mgr = new ProgramManager();
               var output = mgr.AddMetric(GetCallContext(), 
                    GetAddMetricInput());

                if(output.Success)
                    ClearPage();
                else
                {
                    ModelState.AddModelError("MetricName", output.ErrorMessage);
                }
            }
        }

        private void ClearPage()
        {
                ModelState.Clear();
                MetricName = "";
                MetricDesription = "";

        }
        public void OnPostReset()
        {
           ClearPage();
        }

        public void OnPostCancel()
        {
            Response.Redirect("/main");
        }



        private AddMetricInput GetAddMetricInput()
        {
            AddMetricInput input = new AddMetricInput();
            input.MetricDescription = MetricDesription;
            input.MetricName = MetricName;
            input.MetricValueDefinition = new MetricIntegerValueDefinition()
            {
                MinValue = 0,
                MaxValue = 100
            };

            return input;
        }


        [BindProperty] [Required] public string MetricName { get; set; }

        [BindProperty] [Required] public string MetricDesription { get; set; }

        [BindProperty] public Enum MetricType { get; set; }

        [BindProperty]  public int MinValue { get; set; }

        [BindProperty] public int MaxValue { get; set; }

        [BindProperty] public string[] StringRange { get; set; }

        public List<Metric> SavedMetrics
        {
            get
            {
                Debug.WriteLine("Called");

                ProgramManager mgr = new ProgramManager();
                return mgr.GetMetrics(GetCallContext());
            }
        }
    }

    public enum MetricType
    {
        IntegerRage, StringRange
    }
  
}