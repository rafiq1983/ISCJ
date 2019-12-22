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
    public class AssignMetric : BasePageModel
    {
        private List<Metric> _allMetrics = null;
        private List<SelectedMetric> _selectedMetrics = null;
        public void OnGet(string entityType)
        {
            EntityType = entityType;
            LoadPageData();

        }

        private void LoadPageData()
        {
            
            //TODO:
            //We don't need to maintain two collections. 
            //We can create just one collection to display and take input data.
            ProgramManager mgr = new ProgramManager();
            _allMetrics = mgr.GetMetrics(GetCallContext());

            _selectedMetrics = _allMetrics.Select(x => new SelectedMetric() {MetricId = x.MetricId}).ToList();

           var itemsSelectedInDB = mgr.GetEntityMetricsAssociations(GetCallContext(), EntityId).Select(
                x => new SelectedMetric()
                {
                    IsSelected = true,
                    MetricId = x.MetricId
                }).ToList();

           foreach (var item in itemsSelectedInDB )
           {
               var selectedItem = _selectedMetrics.Single(x => x.MetricId == item.MetricId);
               selectedItem.IsSelected = true;
           }

           EntityValue = mgr.GetEntityName(GetCallContext(), EntityId, EntityType);
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                ProgramManager mgr = new ProgramManager();
                var items = GetAssociateMetricInput();

                var output = mgr.AssociateMetrics(GetCallContext(), new SetMetricsToEntityInput()
                {
                     EntityId = EntityId,
                     EntityType = EntityType,
                     MetricIds = SelectedMetrics.Where(x=>x.IsSelected).Select(x=>x.MetricId).ToList()

                });

             
             if(!output.Success)
               {
                   ModelState.AddModelError("", output.ErrorMessage);
               }

            }

            LoadPageData();
        }

        private void ClearPage()
        {
                ModelState.Clear();
                EntityType = string.Empty;
                EntityId = Guid.Empty;

        }
        public void OnPostReset()
        {
           ClearPage();
        }

        public void OnPostCancel()
        {
            Response.Redirect("/main");
        }

        private List<AttachMetricInput> GetAssociateMetricInput()
        {
            List<AttachMetricInput> output = new List<AttachMetricInput>();

            foreach (var item in SelectedMetrics.Where(x=>x.IsSelected))
            {
                output.Add(new AttachMetricInput
                {
                    EntityId = EntityId,
                    EntityType = EntityType,
                    MetricId = item.MetricId
                });
            }

            return output;
        }
        
        //supports get will allow population of Entity id from Url params.
        [BindProperty(SupportsGet=true)] [Required] public Guid EntityId { get; set; }

        [BindProperty(SupportsGet = true)] [Required] public string EntityType { get; set; }

        //Making it enumerable so that Razor calls it once only.
        public List<Metric> DisplayMetrics
        {
            get { return _allMetrics; }
        }



        public string EntityFriendlyName
        {
            get { return EntityType; }
           
        }

        public string EntityValue
        {
            get;
            private set;

        }
        [BindProperty]
        public List<SelectedMetric> SelectedMetrics
        {
            get { return _selectedMetrics; }
            set { _selectedMetrics = value; }
        }
    }
    
  
}