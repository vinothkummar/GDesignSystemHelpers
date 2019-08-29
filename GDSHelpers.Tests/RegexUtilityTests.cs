using Xunit;
using FluentAssertions;
using GDSHelpers;

namespace GDSHelpers.Tests
{
    public class RegexUtilityTests
    {
        [Theory]
        [InlineData("myname@mydomain.com")]
        [InlineData("myname@mydomain.co.uk")]
        [InlineData("MYNAME@MYDOMAIN.COM")]
        [InlineData("MYNAME@MYDOMAIN.CO.UK")]
        public void Email_should_pass(string email)
        {
            var result = RegexUtilities.IsValidEmail(email);
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData("mynamemydomain.com")]
        [InlineData("mynamemydomain.co.uk")]
        [InlineData("myname@mydomaincom.")]
        [InlineData("myname.mydomaincom@com")]
        [InlineData("@.mydomaincom@com")]
        [InlineData("myname@@mydomain.com")]
        [InlineData("myname@mydomain..com")]
        [InlineData("myname@my@domain.com")]
        [InlineData("myname@mydomain.c.o.m")]
        [InlineData("mynamemydomain")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("@.com")]
        [InlineData("@.")]
        public void Email_should_fail(string email)
        {
            var result = RegexUtilities.IsValidEmail(email);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("+447222555555")]
        [InlineData("+44 7222 555 555")]
        [InlineData("(0722) 5555555 #2222")]
        [InlineData("01632960001")]
        [InlineData("01632 960 001")]
        [InlineData("01632 960001")]
        [InlineData("01632960 001")]
        public void PhoneNumber_should_pass(string phoneNumber)
        {
            var result = RegexUtilities.IsValidPhoneNumber(phoneNumber);
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("123()")]
        [InlineData("123(abcdefg)")]
        [InlineData("123abcdefg")]
        [InlineData("abcdefg")]
        [InlineData("(+447222)555555")]
        [InlineData("+44(7222)555555")]
        [InlineData("(0722) 5555555 #22")]
        public void PhoneNumber_should_fail(string phoneNumber)
        {
            var result = RegexUtilities.IsValidPhoneNumber(phoneNumber);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("myname")]        
        [InlineData("firstname lastname")]
        [InlineData("Firstname Lastname")]
        [InlineData("FIRSTNAME LASTNAME")]
        [InlineData("firstname-lastname")]
        [InlineData("firstname o'lastname")]
        [InlineData("FIRSTNAME O'LASTNAME")]        
        public void Name_should_pass(string name)
        {
            var result = RegexUtilities.IsValidName(name);
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData("myname123")]
        [InlineData("firstname1lastname")]
        [InlineData("Firstname&Lastname")]
        [InlineData("Firstname Lastname;")]
        public void Name_should_fail(string name)
        {
            var result = RegexUtilities.IsValidName(name);
            result.Should().BeFalse();
        }
        [Theory]
        [InlineData("abcdefghijklmnopqrstuvwxyz")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("1234567890 loads of text")]
        [InlineData("loads of text")]
        [InlineData("loads of text123")]
        [InlineData("123loads of text")]
        [InlineData("loads of-text")]
        [InlineData("loads of'text")]
        [InlineData("loads of text!")]
        [InlineData("loads of (text)")]
        [InlineData("loads of & text")]
        [InlineData("loads of text+")]
        [InlineData("loads of text=")]
        [InlineData("loads of text:")]
        [InlineData("loads of text/")]
        public void FreeText_should_pass(string text)
        {
            var result = RegexUtilities.IsValidFreeText(text);
            result.Should().BeTrue();
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("loads of text;")]        
        [InlineData("loads of text£")]
        [InlineData("loads of text$")]
        [InlineData("loads of text%")]
        [InlineData("loads of text^")]
        [InlineData("loads of text*")]
        [InlineData("loads of text<")]
        [InlineData("loads of text>")]
        [InlineData("loads of text@")]
        [InlineData("loads of text#")]
        [InlineData("loads of text?")]
        [InlineData("loads of text|")]
        [InlineData(@"loads of text\")]
        [InlineData("loads of text~")]
        [InlineData("loads of text{")]
        [InlineData("loads of text}")]
        [InlineData("loads of text[")]
        [InlineData("loads of text]")]
        public void FreeText_should_fail(string text)
        {
            var result = RegexUtilities.IsValidFreeText(text);
            result.Should().BeFalse();
        }

    }
}
