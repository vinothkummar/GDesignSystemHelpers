using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-paragraph", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ParagraphHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.AddClass("govuk-body", HtmlEncoder.Default);

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }
}
