using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ISCJ.TagHelpers
{
    [HtmlTargetElement("x:FieldDisplayResourceKey", Attributes = "resource-key")]
    public class FieldDisplayResourceKeyTagHelper:TagHelper
    {
        // Can be passed via <email mail-to="..." />. 
        // PascalCase gets translated into kebab-case.
        public string ResourceKey { get; set; }
        public string TagName { get; set; } = "span"; //replaces code with span

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = TagName;
            string desc = "Registration Number";
            output.Content.SetContent(desc);
        }
    }
}
