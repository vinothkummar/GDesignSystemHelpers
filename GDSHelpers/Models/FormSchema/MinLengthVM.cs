using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class MinLengthVM
    {
        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("min")]
        public int Min { get; set; }


        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

    }
}
