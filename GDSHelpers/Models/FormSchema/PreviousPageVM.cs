using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class PreviousPageVM
    {

        [JsonProperty("question_id")]
        public string QuestionId { get; set; }


        [JsonProperty("answer")]
        public string Answer { get; set; }


        [JsonProperty("page_id")]
        public string PageId { get; set; }

    }
}
