using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-phase-banner", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PhaseBannerHelper : TagHelper
    {
        public PhaseBannerHelper()
        {
            Text = "This is a new service – your {0} will help us to improve it.";
            UrlText = "feedback";
            Url = "#";
        }

        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [HtmlAttributeName("url")]
        public string Url { get; set; }

        [HtmlAttributeName("url-text")]
        public string UrlText { get; set; }

        [HtmlAttributeName("phase")]
        public GdsEnums.Phases Phase { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var url = $"<a class=\"govuk-link\" href=\"{Url}\">{UrlText}</a>";
            var mainMsg = string.Format(Text, url);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-phase-banner");

            var sb = new StringBuilder();
            sb.AppendLine("<p class=\"govuk-phase-banner__content\">");
            sb.AppendLine($"<strong class=\"govuk-tag govuk-phase-banner__content__tag\">{Phase.ToString()}</strong>");
            sb.AppendLine($"<span class=\"govuk-phase-banner__text\">{mainMsg}</span>");
            sb.AppendLine("</p>");
            
            output.PostContent.SetHtmlContent(sb.ToString());

        }
    }
}
