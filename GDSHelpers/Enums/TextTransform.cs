using System.ComponentModel;

namespace GDSHelpers
{
    public partial class GdsEnums
    {
        public enum TextTransform
        {
            [Description("No Action")]
            None,
            [Description("initial-caps")]
            InitialCaps,
            [Description("all-caps")]
            AllCaps,
            [Description("all-small")]
            AllSmall,
        }
    }
}

