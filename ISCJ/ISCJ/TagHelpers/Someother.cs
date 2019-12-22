using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ.TagHelpers
{
  [HtmlTargetElement("input", Attributes = "currency-format")]
  public class CurrencyEditTagHelper : TagHelper
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {

      //output.Attributes.Add("type", "text");
      output.Attributes.Remove(output.Attributes["value"]);
      output.Attributes.Add("value", "Currency Format " + "Currency Format");
      base.Process(context, output);
    }
  }
}
