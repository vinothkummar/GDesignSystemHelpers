using System.Text;
using GDSHelpers.Models.FormSchema;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GDSHelpers
{
    public static class InlineHelpers
    {

        /// <summary>
        /// Creates a GDS compliant Question with label, help text and error messages
        /// </summary>
        /// <param name="helper">The HTML Class</param>
        /// <param name="question">The QuestionVM class</param>
        /// <param name="htmlAttributes">Any additional attributes to apply to the questions.</param>
        /// <returns>Returns the HTML for GDS compliant questions</returns>
        public static IHtmlContent GdsQuestion(this IHtmlHelper helper, QuestionVM question, object htmlAttributes = null)
        {
            IHtmlContent content;

            switch (question.InputType)
            {
                case "textbox":
                    content = BuildTextBox(question);
                    break;

                case "textarea":
                    content = BuildTextArea(question);
                    break;

                case "optionlist":
                    content = BuildOptionList(question);
                    break;

                case "selectlist":
                    content = BuildSelectList(question);
                    break;

                case "checkboxlist":
                    content = BuildCheckboxList(question);
                    break;

                default:
                    content = BuildInfoPage(question);
                    break;

            }

            return new HtmlString(content.ToString());

        }

        private static IHtmlContent BuildInfoPage(QuestionVM question)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(question.Question))
                sb.AppendLine($"<p class=\"govuk-body\">{question.Question}</p>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<p class=\"govuk-body\">{question.AdditionalText}</p>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            return new HtmlString(sb.ToString());

        }

        private static IHtmlContent BuildTextBox(QuestionVM question)
        {
            var elementId = question.QuestionId;
            var isErrored = question.Validation?.IsErrored == true;
            var errorMsg = question.Validation?.ErrorMessage;
            var erroredCss = isErrored ? "govuk-form-group--error" : "";
            var erroredInputCss = isErrored ? "govuk-input--error" : "";

            var sb = new StringBuilder();

            sb.AppendLine($"<div class=\"govuk-form-group {erroredCss}\">");
            sb.AppendLine($"<label class=\"govuk-label\" for=\"{elementId}\">{question.Question}</label>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<span id=\"{elementId}-hint\" class=\"govuk-hint\">{question.AdditionalText}</span>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            if (isErrored)
                sb.AppendLine($"<span id=\"{elementId}-error\" class=\"govuk-error-message\">{errorMsg}</span>");

            sb.AppendLine($"<input class=\"govuk-input {erroredInputCss}\" id=\"{elementId}\" name=\"{elementId}\" type=\"{question.DataType}\" aria-describedby=\"{elementId}-hint\" value=\"{question.Answer}\">");

            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        private static IHtmlContent BuildTextArea(QuestionVM question)
        {
            var elementId = question.QuestionId;
            var showCounter = question.Validation?.MaxLength != null;
            var counterCount = question.Validation?.MaxLength?.Max;
            var counterType = question.Validation?.MaxLength?.Type == "words" ? "maxwords" : "maxlength";
            var counterCss = showCounter ? "js-character-count" : "";

            var isErrored = question.Validation?.IsErrored == true;
            var errorMsg = question.Validation?.ErrorMessage;
            var erroredCss = isErrored ? "govuk-form-group--error" : "";
            var erroredInputCss = isErrored ? "govuk-textarea--error" : "";

            var sb = new StringBuilder();

            if (showCounter)
                sb.AppendLine($"<div class=\"govuk-character-count\" data-module=\"character-count\" data-{counterType}=\"{counterCount}\">");

            sb.AppendLine($"<div class=\"govuk-form-group {erroredCss}\">");
            sb.AppendLine($"<label class=\"govuk-label\"  for=\"{elementId}\">{question.Question}</label>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<span id=\"{elementId}-hint\" class=\"govuk-hint\">{question.AdditionalText}</span>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            if (isErrored)
                sb.AppendLine($"<span id=\"{elementId}-error\" class=\"govuk-error-message\">{errorMsg}</span>");

            sb.AppendLine($"<textarea class=\"govuk-textarea {erroredInputCss} {counterCss}\" id=\"{elementId}\" " +
                      $"name=\"{elementId}\" rows=\"5\" aria-describedby=\"{elementId}-hint\">{question.Answer}</textarea>");

            sb.AppendLine("</div>");


            if (showCounter)
            {
                sb.AppendLine($"<span id=\"{elementId}-info\" class=\"govuk-hint govuk-character-count__message\" aria-live=\"polite\"></span>");
                sb.AppendLine("</div>");
            }

            return new HtmlString(sb.ToString());
        }

        private static IHtmlContent BuildOptionList(QuestionVM question)
        {
            var elementId = question.QuestionId;
            var isErrored = question.Validation?.IsErrored == true;
            var errorMsg = question.Validation?.ErrorMessage;
            var erroredCss = isErrored ? "govuk-form-group--error" : "";


            var sb = new StringBuilder();

            sb.AppendLine($"<div class=\"govuk-form-group {erroredCss}\">");
            sb.AppendLine("<fieldset class=\"govuk-fieldset\" aria-describedby=\"changed-name-hint\">");

            sb.AppendLine("<legend class=\"govuk-fieldset__legend govuk-fieldset__legend--xl\">");
            sb.AppendLine($"<label class=\"govuk-label\">{question.Question}</label>");
            sb.AppendLine("</legend>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<span id=\"{elementId}-hint\" class=\"govuk-hint\">{question.AdditionalText}</span>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            if (isErrored)
                sb.AppendLine($"<span id=\"{elementId}-error\" class=\"govuk-error-message\">{errorMsg}</span>");


            var count = 0;
            var inlineCSS = "";
            sb.AppendLine($"<div class=\"govuk-radios {inlineCSS}\">");
            
            foreach (var option in question.Options)
            {
                var value = option.Value;
                var text = option.Text;
                var hint = option.Hint;

                var checkedCss = question.Answer == value ? "checked" : "";

                sb.AppendLine("<div class=\"govuk-radios__item\">");
                sb.AppendLine($"<input class=\"govuk-radios__input\" id=\"{elementId}-{count}\" name=\"{elementId}\" type=\"radio\" value=\"{value}\" {checkedCss}>");
                sb.AppendLine($"<label class=\"govuk-label govuk-radios__label\" for=\"{elementId}-{count}\">{text}</label>");

                if (!string.IsNullOrEmpty(hint))
                    sb.AppendLine($"<span id=\"{elementId}-{count}-item-hint\" class=\"govuk-hint govuk-radios__hint\">{hint}</span>");

                sb.AppendLine("</div>");
                count += 1;

            }

            sb.AppendLine("</div>");



            sb.AppendLine("</fieldset>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());

        }

        private static IHtmlContent BuildSelectList(QuestionVM question)
        {
            var elementId = question.QuestionId;
            var isErrored = question.Validation?.IsErrored == true;
            var errorMsg = question.Validation?.ErrorMessage;
            var erroredCss = isErrored ? "govuk-form-group--error" : "";


            var sb = new StringBuilder();

            sb.AppendLine($"<div class=\"govuk-form-group {erroredCss}\">");
            sb.AppendLine($"<label class=\"govuk-label\" for=\"{elementId}\">{question.Question}</label>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<span id=\"{elementId}-hint\" class=\"govuk-hint\">{question.AdditionalText}</span>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            if (isErrored)
                sb.AppendLine($"<span id=\"{elementId}-error\" class=\"govuk-error-message\">{errorMsg}</span>");


            
            sb.AppendLine($"<select class=\"govuk-select\" id=\"{elementId}\" name=\"{elementId}\">");
            sb.AppendLine("<option value>Please select</option>");
            if (question.Options != null)
            {
                foreach (var option in question.Options)
                {
                    var isSelected = question.Answer == option.Value ? "checked" : "";
                    sb.AppendLine($"<option value=\"{option.Value}\" {isSelected}>{option.Text}</option>");
                }
            }
            sb.AppendLine("</select>");
            sb.AppendLine("</div>");
            

            return new HtmlString(sb.ToString());

        }

        private static IHtmlContent BuildCheckboxList(QuestionVM question)
        {
            var elementId = question.QuestionId;
            var isErrored = question.Validation?.IsErrored == true;
            var errorMsg = question.Validation?.ErrorMessage;
            var erroredCss = isErrored ? "govuk-form-group--error" : "";


            var sb = new StringBuilder();

            sb.AppendLine($"<div class=\"govuk-form-group {erroredCss}\">");
            sb.AppendLine("<fieldset class=\"govuk-fieldset\" aria-describedby=\"changed-name-hint\">");

            sb.AppendLine("<legend class=\"govuk-fieldset__legend govuk-fieldset__legend--xl\">");
            sb.AppendLine($"<label class=\"govuk-label\">{question.Question}</label>");
            sb.AppendLine("</legend>");

            if (!string.IsNullOrEmpty(question.AdditionalText))
                sb.AppendLine($"<span id=\"{elementId}-hint\" class=\"govuk-hint\">{question.AdditionalText}</span>");

            if (!string.IsNullOrEmpty(question.HtmlContent))
                sb.AppendLine(question.HtmlContent);

            if (isErrored)
                sb.AppendLine($"<span id=\"{elementId}-error\" class=\"govuk-error-message\">{errorMsg}</span>");




            sb.AppendLine($"<div class=\"govuk-checkboxes\">");
            if (question.Options != null)
            {
                var count = 0;
                foreach (var option in question.Options)
                {
                    var value = option.Value;
                    var text = option.Text;
                    var hint = option.Hint;

                    var checkedCss = question.Answer == value ? "checked" : "";

                    sb.AppendLine("<div class=\"govuk-checkboxes__item\">");
                    sb.AppendLine($"<input class=\"govuk-checkboxes__input\" id=\"{elementId}-{count}\" name=\"{elementId}\" type=\"checkbox\" value=\"{value}\" {checkedCss}>");
                    sb.AppendLine($"<label class=\"govuk-label govuk-checkboxes__label\" for=\"{elementId}-{count}\">{text}</label>");

                    if (!string.IsNullOrEmpty(hint))
                        sb.AppendLine($"<span id=\"{elementId}-{count}-item-hint\" class=\"govuk-hint govuk-checkboxes__hint\">{hint}</span>");

                    sb.AppendLine("</div>");
                    count += 1;
                }
            }
            sb.AppendLine("</div>");

            
            sb.AppendLine("</fieldset>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());

        }

        public static IHtmlContent GdsButton(this IHtmlHelper helper, string buttonType, string buttonText, object htmlAttributes = null)
        {
            var button = new TagBuilder("button");
            button.Attributes.Add("class", "govuk-button");
            button.Attributes.Add("type", buttonType.ToLower());
            button.InnerHtml.Append(buttonText);

            button.MergeHtmlAttributes(htmlAttributes);

            return button;
        }





        private static void MergeHtmlAttributes(this TagBuilder tagBuilder, object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var customAttribute in customAttributes)
                {
                    tagBuilder.MergeAttribute(customAttribute.Key, customAttribute.Value.ToString());
                }
            }
        }

    }
}
