using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-li" )]
    public class ListItemelper : TagHelper
    {
        [HtmlAttributeName("list-item-id")]
        public string ListItemId { get; set; } 

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(ListItemId) ? "" : ListItemId); 

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}