using System.ComponentModel;

namespace GDSHelpers
{
    public partial class GdsEnums
    {
        public enum InputWidth
        {
            [Description("No Action")]
            None,
            [Description("govuk-input--width-2")]
            Width_2,
            [Description("govuk-input--width-3")]
            Width_3,
            [Description("govuk-input--width-4")]
            Width_4,
            [Description("govuk-input--width-5")]
            Width_5,
            [Description("govuk-input--width-10")]
            Width_10,
            [Description("govuk-input--width-20")]
            Width_20,
            [Description("govuk-input--width-30")]
            Width_30,

        }
    }
}

