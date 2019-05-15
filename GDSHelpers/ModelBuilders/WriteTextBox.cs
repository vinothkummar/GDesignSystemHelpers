using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static GDSHelpers.GdsEnums;

namespace GDSHelpers
{
    public partial class ModelBuilder
    {
        public Autocomplete AutoComplete { get; set; }

        public AddionalOptions Spellcheck { get; set; }

        public TextPattern Pattern { get; set; }

        public string Title { get; set; }

        public AddionalOptions Required { get; set; }

        public TextTransform TextTransform { get; set; }



        public void WriteTextBox(TextWriter writer  )
        {
            var tagBuilder = HtmlGenerator.GenerateTextBox(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                For.Model,
                null,
                new { @class = string.Format( "govuk-input {0}", 
                            TextTransform == TextTransform.None ? "" : GetCssClassFromEnum(TextTransform)) });


            if (this.AutoComplete != Autocomplete.Null)
                tagBuilder.Attributes.Add("autocomplete", GetCssClassFromEnum(this.AutoComplete));
            if (this.Spellcheck != AddionalOptions.None)
                tagBuilder.Attributes.Add("spellcheck", GetCssClassFromEnum(this.Spellcheck));
            if (Pattern != TextPattern.None)
                tagBuilder.Attributes.Add("pattern", GetCssClassFromEnum(Pattern));
            if (!string.IsNullOrEmpty(Title))
                tagBuilder.Attributes.Add("title", Title);
            if (!(Required == AddionalOptions.None || Required == AddionalOptions.False))
                tagBuilder.Attributes.Add("required", GetCssClassFromEnum(Required));

            if (!string.IsNullOrEmpty(For.Metadata.Description))
                tagBuilder.MergeAttribute("aria-describedby", For.GenerateHintId());

            tagBuilder.WriteTo(writer, HtmlEncoder);
        }

    }
}
