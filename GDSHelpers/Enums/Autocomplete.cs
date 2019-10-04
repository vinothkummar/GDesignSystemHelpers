using System.ComponentModel;

namespace GDSHelpers
{
    public partial class GdsEnums
    {
        public enum Autocomplete
        {
            [Description("No Action")]
            Null,
            [Description("off")]
            Off,
            [Description("on")]
            On,
            [Description("name")]
            Fullname,
            [Description("honorific-prefix")]
            HonorificPrefix,
            [Description("given-name")]
            FirstName,
            [Description("additional-name")]
            MiddleName,
            [Description("family-name")]
            FamilyName,
            [Description("nickname")]
            NickName,
            [Description("username")]
            UserName,
            [Description("new-password")]
            NewPassword,
            [Description("current-password")]
            CurrentPassword,
            [Description("organization")]
            Organization,
            [Description("street-address")]
            StreetAddress,
            [Description("address-line1")]
            AddressLine1,
            [Description("address-line2")]
            AddressLine2,
            [Description("address-line3")]
            AddressLine3,
            [Description("country")]
            CountryCode,
            [Description("country-name")]
            CountryName,
            [Description("postal-code")]
            PostalCode,
            [Description("address-level1")]
            AddressLevel1,
            [Description("address-level2")]
            AddressLevel2,
            [Description("email")]
            Email,
            [Description("tel")]
            Telephone,
            [Description("photo")]
            Photo,
            [Description("url")]
            Url,
            [Description("sex")]
            Sex,
            [Description("bday")]
            FullDate,
            [Description("bday-day")]
            DateDay,
            [Description("bday-month")]
            DateMonth,
            [Description("bday-year")]
            DateYear,
        }
    }
}

