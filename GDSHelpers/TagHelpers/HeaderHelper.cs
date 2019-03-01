using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-header")]
    public class HeaderHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "header";
            output.Attributes.SetAttribute("class", "govuk-header");
            output.Attributes.SetAttribute("role", "banner");
            output.Attributes.SetAttribute("data-module", "header");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}
