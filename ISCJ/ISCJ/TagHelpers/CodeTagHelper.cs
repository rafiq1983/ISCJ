using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ.TagHelpers
{
    [HtmlTargetElement("x:Code")]
    public class CodeTagHelper:TagHelper
  {
    public CodeTagHelper(TypeToNameService typeToNameService)
    {
      TypeToNameService = typeToNameService;
    }

    // Can be passed via <email mail-to="..." />. 
    // PascalCase gets translated into kebab-case.
    public string CodeType { get; set; }

    public string Code { get; set; }
    public TypeToNameService TypeToNameService { get; }

    public string TagName { get; set; } = "span"; //replaces code with span

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagName ; 
      string desc = TypeToNameService.GetDescription(CodeType, Code);

     
      output.Content.SetContent(desc);
    }
  }
}
