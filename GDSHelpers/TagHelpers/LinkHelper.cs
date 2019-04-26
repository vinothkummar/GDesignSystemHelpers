using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{
    [HtmlTargetElement("gds-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class LinkHelper : TagHelper
    {
        [HtmlAttributeName("link-id")]
        public string ListItemId { get; set; } 
         
        [HtmlAttributeName("link-text")]
        public string LinkText { get; set; } 

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(ListItemId) ? "" : ListItemId); 
             
            output.Content.SetContent(LinkText);

        }
    }
}
