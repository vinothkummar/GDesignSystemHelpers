using System.Collections.Generic;

namespace GDSHelpers.Models
{
    public class BreadCrumbs
    {
        public List<Crumb> Crumbs { get; set; }

    }

    public class Crumb
    {
        public string Url { get; set; }
        public string Text { get; set; }
    }

}
