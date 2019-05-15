using System.Collections.Generic;
using Newtonsoft.Json;

namespace GDSHelpers.Models.FormSchema
{
    public class PageVM
    {
        [JsonProperty("page_id")]
        public string PageId { get; set; }


        [JsonProperty("page_name")]
        public string PageName { get; set; }


        [JsonProperty("pre_amble")]
        public string PreAmble { get; set; }


        [JsonProperty("questions")]
        public IEnumerable<QuestionVM> Questions { get; set; }


        [JsonProperty("post_amble")]
        public string PostAmble { get; set; }


        [JsonProperty("buttons")]
        public IEnumerable<ButtonVM> Buttons { get; set; }


        [JsonProperty("next_page_id")]
        public string NextPageId { get; set; }


        [JsonProperty("previous_pages")]
        public IEnumerable<PreviousPageVM> PreviousPages { get; set; }


        [JsonProperty("previous_page_id")]
        public string PreviousPageId { get; set; }

    }
}
