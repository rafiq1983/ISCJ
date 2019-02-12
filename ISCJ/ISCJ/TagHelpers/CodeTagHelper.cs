using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ.TagHelpers
{
  public class CodeTagHelper:TagHelper
  {
    public CodeTagHelper(TypeToNameService typeToNameService)
    {
      TypeToNameService = typeToNameService;
    }
    private const string EmailDomain = "contoso.com";

    // Can be passed via <email mail-to="..." />. 
    // PascalCase gets translated into kebab-case.
    public string CodeType { get; set; }

    public string Code { get; set; }
    public TypeToNameService TypeToNameService { get; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = "span";    // Replaces <email> with <a> tag
      string desc = TypeToNameService.GetDescription(CodeType, Code);

     
      output.Content.SetContent(desc);
    }
  }
}
