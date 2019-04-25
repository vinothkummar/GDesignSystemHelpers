using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-back-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class BackLinkHelper : TagHelper
    {
        [HtmlAttributeName("backlink-id")]
        public string BackLinkId { get; set; }

        [HtmlAttributeName("link-text")]
        public string LinkText { get; set; } 

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(BackLinkId) ? "" : BackLinkId);
            output.Attributes.SetAttribute("class", "govuk-back-link"); 
            output.Content.SetContent(LinkText);

        }
    }
}
