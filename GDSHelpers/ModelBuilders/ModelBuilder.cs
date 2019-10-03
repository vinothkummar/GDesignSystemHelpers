using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GDSHelpers
{
    public partial class ModelBuilder
    {
        public IHtmlGenerator HtmlGenerator { get; set; }
        public HtmlEncoder HtmlEncoder { get; set; }
        public ModelExpression For { get; set; }
        public ViewContext ViewContext { get; set; }



        #region Label
        public void WriteLabel(TextWriter writer, string hiddenSpan = "")
        {
            var tagBuilder = HtmlGenerator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                new { @class = "govuk-label" });

            if (!string.IsNullOrWhiteSpace(hiddenSpan) && hiddenSpan.Length>1)
                tagBuilder.InnerHtml.AppendHtml($"<span class=\"govuk-visually-hidden\">{hiddenSpan}</span>");

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }
        #endregion
        #region WriteHint
        public void WriteHint(TextWriter writer)
        {
            var lbl = new TagBuilder("span");
            lbl.MergeAttribute("id", For.GenerateHintId());
            lbl.MergeAttribute("class", "govuk-hint");
            lbl.InnerHtml.Append(For.Metadata.Description);
            lbl.WriteTo(writer, HtmlEncoder);
        }
        public void WriteValidation(TextWriter writer)
        {
            var tagBuilder = HtmlGenerator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                message: null,
                tag: null,
                htmlAttributes: new { @class = "govuk-error-message" });

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }
        public void WriteCountInfo(TextWriter writer)
        {
            var lbl = new TagBuilder("span");
            lbl.MergeAttribute("id", For.GenerateInfoId());
            lbl.MergeAttribute("class", "govuk-hint govuk-character-count__message");
            lbl.MergeAttribute("aria-live", "polite");
            lbl.InnerHtml.Append("");
            lbl.WriteTo(writer, HtmlEncoder);
        }

        #endregion


   


        #region WriteTextArea
        public void WriteTextArea(TextWriter writer, bool addCounter = false)
        {
            var counterCss = addCounter ? " js-character-count" : "";

            var tagBuilder = HtmlGenerator.GenerateTextArea(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                5,
                80,
                new { @class = "govuk-textarea" + counterCss });

            if (!string.IsNullOrEmpty(For.Metadata.Description))
                tagBuilder.MergeAttribute("aria-describedby", For.GenerateHintId());
            if (MaxLength != 0)
                tagBuilder.MergeAttribute("maxlength", this.MaxLength.ToString());

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }
        #endregion

        #region WriteSelect
        public void WriteSelect(TextWriter writer, List<SelectListItem> listItems, string optionLabel)
        {
            var tagBuilder = HtmlGenerator.GenerateSelect(
                ViewContext,
                For.ModelExplorer,
                optionLabel,
                For.Name,
                listItems,
                false,
                new { @class = "govuk-select" });

            if (!string.IsNullOrEmpty(For.Metadata.Description))
                tagBuilder.MergeAttribute("aria-describedby", For.GenerateHintId());

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }
        #endregion
    }
}
