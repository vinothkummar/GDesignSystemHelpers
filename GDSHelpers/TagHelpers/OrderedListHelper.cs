using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-ol"), RestrictChildren("gds-li")]
    public class OrderedListHelper : TagHelper
    {
        [HtmlAttributeName("list-id")]
        public string ListId { get; set; }

        [HtmlAttributeName("list-styles")]
        public string ListStyles { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ol";
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(ListId) ? "" : ListId);
            output.Attributes.SetAttribute("class", string.IsNullOrEmpty(ListStyles) ? "" : ListStyles);

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}