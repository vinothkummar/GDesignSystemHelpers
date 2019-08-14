using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class RequiredIfVM
    {  
        [JsonProperty("inclusive_logic")]
        public string InclusiveLogic { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("linked_ids")]
        public string LinkedIds { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
