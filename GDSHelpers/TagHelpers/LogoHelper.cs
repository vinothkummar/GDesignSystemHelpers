using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-logo", ParentTag = "gds-header-container")]
    public class LogoHelper : TagHelper
    {
        [HtmlAttributeName("url")]
        public string Url { get; set; }

        [HtmlAttributeName("url-title")]
        public string UrlTitle { get; set; }

        [HtmlAttributeName("alt-text")]
        public string AltText { get; set; }

        [HtmlAttributeName("svg-path")]
        public string SvgPath { get; set; }

        [HtmlAttributeName("role")]
        public string Role { get; set; }

        [HtmlAttributeName("focusable")]
        public string Focusable { get; set; }

        [HtmlAttributeName("image-url")]
        public string ImageUrl { get; set; }

        [HtmlAttributeName("svg-viewbox")]
        public string SvgViewBox{ get; set; }

        [HtmlAttributeName("svg-image-height")]
        public string SvgImageHeight { get; set; }

        [HtmlAttributeName("svg-image-width")]
        public string SvgImageWidth { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-header__logo");

            var urlTitle = string.IsNullOrEmpty(UrlTitle) ? "" : $"title=\"{UrlTitle}\"";
            var altText = string.IsNullOrEmpty(AltText) ? "" : $"alt=\"{AltText}\"";
            var svgPath = string.IsNullOrEmpty(SvgPath) ? "" : $"{SvgPath}";
            var role = string.IsNullOrEmpty(Role) ? "" : $"role=\"{Role}\"";
            var focusable = string.IsNullOrEmpty(Focusable) ? "" : $"focusable=\"{Focusable}\"";
            var svgViewBox = string.IsNullOrEmpty(SvgViewBox) ? "" : $"viewBox=\"{SvgViewBox}\"";
            var svgImageHeight = string.IsNullOrEmpty(SvgImageHeight) ? "" : $"height=\"{SvgImageHeight}\"";
            var svgImageWidth = string.IsNullOrEmpty(SvgImageWidth) ? "" : $"width=\"{SvgImageWidth}\"";

            var sb = new StringBuilder();            
            sb.AppendLine($"<a href=\"{Url}\" class=\"govuk-header__link govuk-header__link--homepage\" {urlTitle}>");
            sb.AppendLine($"<span class=\"govuk-header_logotype\">");
            sb.AppendLine($"<svg {role} {focusable} {altText} class=\"govuk-header__logotype-cqc\" xmlns=\"http://www.w3.org/2000/svg\" {svgViewBox} {svgImageHeight} {svgImageWidth}>");
            sb.AppendLine($"{svgPath}");
            sb.AppendLine($"<image src=\"{ImageUrl}\" class=\"govuk-header__logotype-cqc-fallback-image\" {altText} />");
            sb.AppendLine("</svg>");
            sb.AppendLine("</span>");
            sb.AppendLine("</a>");

            output.PostContent.SetHtmlContent(sb.ToString());
        }
    }

}
