using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class RegexVM
    {
        [JsonProperty("regex_string")]
        public string RegexString { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
