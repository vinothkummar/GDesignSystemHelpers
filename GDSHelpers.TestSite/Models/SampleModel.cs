using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GDSHelpers.TestSite.Models
{
    public class SampleModel
    {
        public string Name { get; set; }

        //[Display(Name = "Interesting fact")]
        [DisplayName("Interesting fact")]
        public string InterestingFact { get; set; }
    }

    public class ChildOfSampleModel : SampleModel
    {
        [DisplayName("This is even more interesting fact")]
        public new string InterestingFact { get; set; }
    }
}
