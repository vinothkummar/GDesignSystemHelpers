using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-footer-container", ParentTag = "gds-footer")]
    public class FooterContainerHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-footer__container govuk-width-container");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}
