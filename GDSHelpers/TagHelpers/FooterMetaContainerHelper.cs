using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-footer-container-meta", ParentTag = "gds-footer-container")]
    public class FooterMetaContainerHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-footer__meta");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}
