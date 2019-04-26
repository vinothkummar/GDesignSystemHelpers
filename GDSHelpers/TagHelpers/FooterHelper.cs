using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-footer")]
    public class FooterHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "footer";
            output.Attributes.SetAttribute("class", "govuk-footer");
            output.Attributes.SetAttribute("role", "contentinfo");
            output.Attributes.SetAttribute("data-module", "footer");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}
