using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ.TagHelpers
{
  [HtmlTargetElement("input", Attributes = "auto-complete-url")]
  public class AutoCompleteTagHelper : TagHelper
  {
    public string SearchUrl { get; set; }

    public string AutoCompleteUrl { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      //output.Attributes.Add("type", "text");
      //output.Attributes.Remove(output.Attributes["value"]);
      //output.Attributes.Add("value", "This is my custom Auto Complete with url " + AutoCompleteUrl);

      output.Attributes.Add("data-autocomplete-url", AutoCompleteUrl);

      base.Process(context, output);
    }
  }

}
