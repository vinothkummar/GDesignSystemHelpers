using System.IO;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{
    [HtmlTargetElement("gds-date-box", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DateBoxHelper : TagHelper
    {
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly HtmlEncoder _htmlEncoder;

        public DateBoxHelper(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            this._htmlGenerator = htmlGenerator;
            this._htmlEncoder = htmlEncoder;
        }

        [HtmlAttributeName("datebox-id")]
        public string DateBoxId { get; set; }

        [HtmlAttributeName("autocomplete")]
        public string AutoComplete { get; set; }

        [HtmlAttributeName("width-class")]
        public string WidthCssClass { get; set; }

        [HtmlAttributeName("max-length")]
        public string MaxLength { get; set; }


        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("hide-label")]
        public bool HideLabel { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("govuk-date-input__item", HtmlEncoder.Default);

            output.PreContent.SetHtmlContent($@"<div class=""govuk-form-group"" id=""{DateBoxId}"">");

            using (var writer = new StringWriter())
            {
                if (!HideLabel)
                {
                    var labelBuilder = _htmlGenerator.GenerateLabel(
                        ViewContext,
                        For.ModelExplorer,
                        For.Name,
                        null,
                        new { @class = "govuk-label govuk-date-input__label" });
                    labelBuilder.WriteTo(writer, _htmlEncoder);
                }

                var textboxBuilder = _htmlGenerator.GenerateTextBox(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    For.Model,
                    null,
                    new { @class = "govuk-input govuk-date-input__input", type = "number" });

                if (!string.IsNullOrEmpty(this.AutoComplete))
                {
                    textboxBuilder.MergeAttribute("autocomplete", AutoComplete);
                }

                if (!string.IsNullOrEmpty(this.WidthCssClass))
                {
                    textboxBuilder.AddCssClass(WidthCssClass);
                }

                if (!string.IsNullOrEmpty(this.MaxLength))
                {
                    textboxBuilder.MergeAttribute("max-length", MaxLength);
                }

                if (!string.IsNullOrEmpty(For.Name))
                {
                    textboxBuilder.MergeAttribute("aria-describedby", For.Name);
                }

                if (!string.IsNullOrEmpty(DateBoxId))
                { textboxBuilder.MergeAttribute("aria-labelledby", DateBoxId); }

                textboxBuilder.WriteTo(writer, _htmlEncoder);

                output.Content.SetHtmlContent(writer.ToString());

                output.PostContent.SetHtmlContent(@"</div>");
            }
        }
    }
}

