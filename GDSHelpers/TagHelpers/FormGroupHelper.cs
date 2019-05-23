using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace GDSHelpers.TagHelpers
{
    [HtmlTargetElement("gds-form-group")]
    public class FormGroupHelper : TagHelper
    {
        [HtmlAttributeName("isError")]
        public bool IsError { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("govuk-form-group", HtmlEncoder.Default);
            if (IsError)
                output.AddClass("govuk-form-group--error", HtmlEncoder.Default);
            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}
