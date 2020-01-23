using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MA.Core;

namespace ISCJ.TagHelpers
{
    [HtmlTargetElement("x:label", Attributes="id")]
    public class LabelTagHelper : TagHelper
    {
        private ResourceService _resourceSvc = null;

        public LabelTagHelper(ResourceService resourceSvc)
        {
            _resourceSvc = resourceSvc;
           
        }

        public string TagName { get; set; } = "label"; //replaces code with span
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = TagName;
            output.Content.SetContent(_resourceSvc.GetLabelName(Id));
        }
    }
}