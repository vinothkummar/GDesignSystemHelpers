using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GDSHelpers.TagHelpers
{

    [HtmlTargetElement("gds-input-file", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FileHelper : TagHelper
    {
        public FileHelper()
        {
             
        }

        [HtmlAttributeName("file-id")]
        public string FileId { get; set; }

        [HtmlAttributeName("accept-types")]
        public string AcceptTypes { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";

            output.Attributes.SetAttribute("id", string.IsNullOrEmpty(FileId) ? "" : FileId);
            output.Attributes.SetAttribute("name", "files");
            output.AddClass("govuk-file-upload", HtmlEncoder.Default);
          

            output.Attributes.SetAttribute("type", "file");
            if (!string.IsNullOrEmpty(AcceptTypes))
                output.Attributes.SetAttribute("accept", AcceptTypes);

        }
    }
}
