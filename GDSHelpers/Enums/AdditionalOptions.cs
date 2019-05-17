using System.ComponentModel;

namespace GDSHelpers
{
    public partial class GdsEnums
    {
        public enum AdditionalOptions
        {
            [Description("No option")]
            None,
            [Description("true")]
            True,
            [Description("false")]
            False,
        }
    }
}
