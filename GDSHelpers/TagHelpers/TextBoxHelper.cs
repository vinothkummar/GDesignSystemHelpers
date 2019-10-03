using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using static GDSHelpers.GdsEnums;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-text-box", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TextBoxHelper : TagHelper
    {
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly HtmlEncoder _htmlEncoder;

        public TextBoxHelper(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            _htmlGenerator = htmlGenerator;
            _htmlEncoder = htmlEncoder;
        }

        [HtmlAttributeName("textbox-id")]
        public string TextBoxId { get; set; }

  
        [HtmlAttributeName("autocomplete")]
        public Autocomplete AutoComplete { get; set; }

        [HtmlAttributeName("spellcheck")]
        public AdditionalOptions Spellcheck { get; set; }

        [HtmlAttributeName("text-transform")]
        public TextTransform TextTransform { get; set; }

        [HtmlAttributeName("input-width-chars")]
        public string InputWidthChars { get; set; }

        [HtmlAttributeName("hidden-span")]
        public string HiddenSpan { get; set; }

        [HtmlAttributeName("max-length")]
        public int MaxLength{ get; set; }

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(TextBoxId) ? "" : TextBoxId);

            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var entry);

            output.AddClass("govuk-form-group", HtmlEncoder.Default);
            if (entry?.Errors?.Count > 0)
            {
                output.AddClass("govuk-form-group--error", HtmlEncoder.Default);
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
                modelBuilder.WriteLabel(writer, HiddenSpan);

                if (!string.IsNullOrEmpty(For.Metadata.Description))
                    modelBuilder.WriteHint(writer);

                modelBuilder.WriteValidation(writer);

                modelBuilder.TextBoxId = TextBoxId;
                modelBuilder.AutoComplete = AutoComplete;
                modelBuilder.Spellcheck = Spellcheck;
                modelBuilder.MaxLength = MaxLength;
                modelBuilder.TextTransform = TextTransform;
                modelBuilder.TextBoxWidthChars = InputWidthChars;
                modelBuilder.WriteTextBox(writer);
               
                output.Content.SetHtmlContent(writer.ToString());
            }

        }
        
    }
}