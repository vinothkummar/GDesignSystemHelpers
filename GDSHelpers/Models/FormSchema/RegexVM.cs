using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class RegexVM
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("regex")]
        public string Regex { get; set; }

        [JsonProperty("expression")]
        public string Expression { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
