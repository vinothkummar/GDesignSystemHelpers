using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-footer-container-meta-item", ParentTag = "gds-footer-container-meta")]
    public class FooterMetaItemContainerHelper : TagHelper
    {
        [HtmlAttributeName("additional-styles")]
        public string AdditionalStyles { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (string.IsNullOrEmpty(AdditionalStyles))
            {
                output.Attributes.SetAttribute("class", "govuk-footer__meta-item");
            }
            else
            {
                output.Attributes.SetAttribute("class", "govuk-footer__meta-item " + AdditionalStyles);
            }

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}