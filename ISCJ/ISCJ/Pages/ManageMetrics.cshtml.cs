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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;

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
                mgr.AddMetric(GetCallContext(), 
                    GetAddMetricInput());
            }
        }

        public void OnPostReset()
        {
            ModelState.Clear();
            MetricName = "";
            MetricDesription = "";
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