using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Encodings.Web;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-container")]
    public class ContainerHelper : TagHelper
    {
  
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("govuk-width-container", HtmlEncoder.Default);
            output.TagMode = TagMode.StartTagAndEndTag;
           
        }
    }

}
