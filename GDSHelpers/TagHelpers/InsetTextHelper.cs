using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-inset-text", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InsetTextHelper : TagHelper
    {

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-inset-text");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }
}
