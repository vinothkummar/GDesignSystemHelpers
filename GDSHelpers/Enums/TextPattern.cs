using System.ComponentModel;

namespace GDSHelpers
{
    public partial class GdsEnums
    {
        public enum TextPattern
        {
            [Description("No Action")]
            None,
            [Description("[A-Za-z ]+")]
            TextOnly,
            [Description("[0-9 ]+")]
            NumericOnly,
            //Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters
            [Description("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}")]
            Password,
            [Description("[a-z0-9._ % +-] +@[a-z0-9.-]+\\.[a-z]{2,}$")]
            Email,
        }
    }
}

 