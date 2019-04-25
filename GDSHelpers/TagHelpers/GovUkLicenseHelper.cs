using System.Text; 
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-govuk-license", ParentTag ="gds-footer-container-meta-item")] 
    public class GovUkLicenseHelper : TagHelper
    { 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            output.Attributes.SetAttribute("class", "govuk-footer__licence-description" );

            var sb = new StringBuilder();
            var href = "https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"; 

            sb.Append($"All content is available under the ");
            sb.Append($"<a title=\"Open Government Licence v3.0 Link\" class=\"govuk-footer__link\" href=\"{href}\" rel='license'>Open Government Licence v3.0</a>");
            sb.Append($", except where otherwise stated"); 

            output.PostContent.SetHtmlContent(sb.ToString());
        }
    }

}