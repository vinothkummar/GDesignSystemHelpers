using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-container")]
    public class ContainerHelper : TagHelper
    {
        [HtmlAttributeName("class")]
        public string CustomClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"govuk-width-container {(string.IsNullOrEmpty(CustomClass) ? "" : CustomClass)}");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }

}
