using System.IO;
using static GDSHelpers.GdsEnums;

namespace GDSHelpers
{
    public partial class ModelBuilder
    {
        public Autocomplete AutoComplete { get; set; }

        public AdditionalOptions Spellcheck { get; set; }

        public string TextBoxId { get; set; }

        public TextTransform TextTransform { get; set; }
        public string TextBoxWidthChars {get; set;}
        public void WriteTextBox(TextWriter writer  )
        {
            var tagBuilder = HtmlGenerator.GenerateTextBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                For.Model,
                null,
                new { @class = string.Format("govuk-input {0} {1}", 
                            TextTransform == TextTransform.None ? "" : GetDescriptionFromEnum(TextTransform),
                            string.IsNullOrWhiteSpace(TextBoxWidthChars) ? "" : $"govuk-input--width-{TextBoxWidthChars}")
                    });
            
            if (this.AutoComplete != Autocomplete.Null)
                tagBuilder.MergeAttribute("autocomplete", GetDescriptionFromEnum(this.AutoComplete));
            if (Spellcheck != AdditionalOptions.None)
                tagBuilder.MergeAttribute("spellcheck", GetDescriptionFromEnum(this.Spellcheck));
            if (!string.IsNullOrEmpty(For.Name))
                tagBuilder.MergeAttribute("aria-describedby", For.Name);
            if (!string.IsNullOrEmpty(TextBoxId))
                tagBuilder.MergeAttribute("aria-labelledby", TextBoxId);

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }

    }
}
