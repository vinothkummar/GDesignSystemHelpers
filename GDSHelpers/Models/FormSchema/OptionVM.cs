using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class OptionVM
    {

        [JsonProperty("value")]
        public string Value { get; set; }


        [JsonProperty("text")]
        public string Text { get; set; }



        [JsonProperty("hint")]
        public string Hint { get; set; }

    }
}
