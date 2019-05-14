using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //private readonly Autocomplete _autoComplete ;
        public TextBoxHelper(IHtmlGenerator htmlGenerator, HtmlEncoder htmlEncoder)
        {
            _htmlGenerator = htmlGenerator;
            _htmlEncoder = htmlEncoder;
           // _autoComplete = Autocomplete.Null;
        }

        [HtmlAttributeName("textbox-id")]
        public string TextBoxId { get; set; }

  
        [HtmlAttributeName("autocomplete")]
        public Autocomplete AutoComplete { get; set; }

        [HtmlAttributeName("spellcheck")]
        public AddionalOptions Spellcheck { get; set; }

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //public Autocomplete AutoComplete1 => _autoComplete;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(TextBoxId) ? "" : TextBoxId);

            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out var entry);
            var cssClass = entry?.Errors?.Count > 0 ? "govuk-form-group govuk-form-group--error" : "govuk-form-group";
            output.Attributes.Add("class", cssClass);
           // output.AddClass(cssClass, HtmlEncoder.Default);

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

                if (!string.IsNullOrEmpty(For.Metadata.Description))
                    modelBuilder.WriteHint(writer);
                string autoComplete = AutoComplete == Autocomplete.Null ? "" : GetCssClassFromEnum(AutoComplete);
                string spellCheck = Spellcheck == AddionalOptions.None ? "" : GetCssClassFromEnum(Spellcheck);


                modelBuilder.WriteTextBox(writer, autoComplete, spellCheck);
                modelBuilder.WriteValidation(writer);
                output.Content.SetHtmlContent(writer.ToString());
            }

        }
        
    }
}


