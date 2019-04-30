using System.Text; 
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-govuk-license", ParentTag ="gds-footer-container-meta-item")] 
    public class GovUkLicenseHelper : TagHelper
    {


        [HtmlAttributeName("gov-license-url")]
        public string Url { get; set; }
        [HtmlAttributeName("gov-license-version")]
        public string Version { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            output.Attributes.SetAttribute("class", "govuk-footer__licence-description" );

            var sb = new StringBuilder(); 

            sb.Append($"All content is available under the ");
            sb.Append($"<a title=\"Open Government Licence Link\" class=\"govuk-footer__link\" href=\"{Url}\" rel='license'>Open Government Licence {Version}</a>");
            sb.Append($", except where otherwise stated"); 

            output.PostContent.SetHtmlContent(sb.ToString());
        }
    }

}