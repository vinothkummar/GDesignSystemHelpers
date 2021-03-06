﻿using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-text-area", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TextAreaHelper : TagHelper
    {
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly HtmlEncoder _htmlEncoder;
        
        public TextAreaHelper(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            _htmlGenerator = htmlGenerator;
            _htmlEncoder = htmlEncoder;
            CountType = GdsEnums.CountTypes.None;
        }

        [HtmlAttributeName("textarea-id")]
        public string TextAreaId { get; set; }

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        [HtmlAttributeName("count-type")]
        public GdsEnums.CountTypes CountType { get; set; }

        [HtmlAttributeName("max-length")]
        public int MaxLength { get; set; }

        [HtmlAttributeName("threshold")]
        public int Threshold { get; set; } = 0;

        [HtmlAttributeName("hint-text")]
        public string HintText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(TextAreaId) ? "" : TextAreaId+"div");

            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var entry);
            var cssClass = entry?.Errors?.Count > 0 ? "govuk-form-group govuk-form-group--error" : "govuk-form-group";

            var useCounter = (CountType != GdsEnums.CountTypes.None);

            if (useCounter)
            {
                output.Attributes.Add("class", "govuk-character-count");
                output.Attributes.Add("data-module", "character-count");

                if (Threshold > 0)
                    output.Attributes.Add("data-threshold", Threshold);

                switch (CountType)
                {
                    case GdsEnums.CountTypes.Characters:
                        output.Attributes.Add("data-maxlength", MaxLength);
                        break;

                    case GdsEnums.CountTypes.Words:
                        output.Attributes.Add("data-maxwords", MaxLength);
                        break;
                }

                var divStart = $"<div class=\"{cssClass}\">";
                output.PreContent.AppendHtml(divStart);

                output.PostContent.AppendHtml("</div>");
            }
            else
            {
                output.Attributes.Add("class", cssClass);
            }
            
            var modelBuilder = new ModelBuilder
            {
                For = For,
                ViewContext = ViewContext,
                HtmlEncoder = _htmlEncoder,
                HtmlGenerator = _htmlGenerator
            };
            
            using (var writer = new StringWriter())
            {
                modelBuilder.WriteLabel(writer);

                if (!string.IsNullOrEmpty(For.Metadata.Description) || !string.IsNullOrEmpty(HintText))
                    modelBuilder.WriteHint(writer, HintText);

                
                modelBuilder.WriteValidation(writer);

                modelBuilder.MaxLength = MaxLength;
                modelBuilder.WriteTextArea(writer, useCounter);

                if (CountType != GdsEnums.CountTypes.None)
                    modelBuilder.WriteCountInfo(writer);

               

                output.Content.SetHtmlContent(writer.ToString());
            }

        }
        
    }
}
