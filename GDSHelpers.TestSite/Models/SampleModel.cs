using System.ComponentModel.DataAnnotations;

namespace GDSHelpers.TestSite.Models
{
    public class SampleModel
    {
        public string Name { get; set; }

        [Display(Name = "Interesting fact")]
        public string InterestingFact { get; set; }
    }
}
