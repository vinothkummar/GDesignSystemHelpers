using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using GDSHelpers.Extensions;
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

        /// <summary>
        /// Will be true if view declares a base class, but you are passing a derived class
        /// </summary>
        public bool IsViewModelTypeDifferentThanRuntimeType
        {
            get
            {
                var viewModelType = For.Metadata.ContainerType.Name;
                var runtimeModelType = For.ModelExplorer.Container.ModelType.Name;
                bool result = (viewModelType != runtimeModelType);
                return result;
            }
        }

        #region Label

        /// <summary>
        /// Original code that is generating the label
        /// </summary>
        protected void GenerateLabelHtml(TextWriter writer, string hiddenSpan = "", string customLabel = "")
        {
            var tagBuilder = HtmlGenerator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                string.IsNullOrEmpty(customLabel) ? null : customLabel,
                new { @class = "govuk-label" });

            if (!string.IsNullOrWhiteSpace(hiddenSpan) && hiddenSpan.Length > 1)
                tagBuilder.InnerHtml.AppendHtml($"<span class=\"govuk-visually-hidden\">{hiddenSpan}</span>");

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }

        /// <summary>
        /// Updated version of generating label, it's checking if runtime model type is = view model type
        /// if not it tries to get displayName attribute from runtime
        /// </summary>
        public void WriteLabel(TextWriter writer, string hiddenSpan = "", string customLabel = "")
        {
            //check if model declared in View is different than actual model
            if (string.IsNullOrEmpty(customLabel) 
                && IsViewModelTypeDifferentThanRuntimeType)
            {
                //using reflection we get displayName attr from runtime Model
                customLabel = GetDisplayNameAttributeFromProperty(For.Name);
            }

            GenerateLabelHtml(writer, hiddenSpan, customLabel);
        }

        /// <summary>
        /// Tries to get DisplayNameAttribute or DisplayAttribute from runtime model property
        /// </summary>
        private string GetDisplayNameAttributeFromProperty(string propertyName)
        {
            //it's an actual runtime model, not viewModel declared on view
            MemberInfo property = For.ModelExplorer.Container.ModelType.GetProperty(propertyName);
            return property?.GetCustomAttribute(typeof(DisplayNameAttribute)) is DisplayNameAttribute dd
                ? dd.DisplayName
                : property?.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute da
                    ? da.Name
                    : null;
        }

        #endregion
        #region WriteHint
        public void WriteHint(TextWriter writer, string description ="")
        {
            var lbl = new TagBuilder("span");
            lbl.MergeAttribute("id", For.GenerateHintId());
            lbl.MergeAttribute("class", "govuk-hint");
            lbl.InnerHtml.Append(For.Metadata.Description ?? description);
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
