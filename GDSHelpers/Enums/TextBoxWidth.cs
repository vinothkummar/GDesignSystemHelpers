using System.ComponentModel;

namespace GDSHelpers.Enums
{
    public partial class GdsEnums
    {
        public enum TextBoxWidth
        {
            [Description("govuk-input--width-20")]
            Char20,

            [Description("govuk-input--width-10")]
            Char10,

            [Description("govuk-input--width-5")]
            Char5,

            [Description("govuk-input--width-4")]
            Char4,

            [Description("govuk-input--width-3")]
            Char3,

            [Description("govuk-input--width-2")]
            Char2,

            [Description("govuk-!-width-full")]
            FluidFull,

            [Description("govuk-!-width-three-quarters")]
            FluidThreeQuarters,

            [Description("govuk-!-width-two-thirds")]
            FluidTwoThirds,

            [Description("govuk-!-width-one-half")]
            FluidOneHalf,

            [Description("govuk-!-width-one-third")]
            FluidOneThird,

            [Description("govuk-!-width-one-quarter")]
            FluidOneQuarter
        }
    }
}
