using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GDSHelpers.Tests
{
    public class GDSValidationTests
    {
        private static readonly HashSet<char> allowedChars = new HashSet<char>(@"1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,'()?!#$£%^@*;:+=_-/ ");
        private static readonly List<string> restrictedWords = new List<string> { "javascript", "onblur", "onchange", "onfocus", "onfocusin", "onfocusout", "oninput", "onmouseenter", "onmouseleave",
            "onselect", "onclick", "ondblclick", "onkeydown", "onkeypress", "onkeyup", "onmousedown", "onmousemove", "onmouseout", "onmouseover", "onmouseup", "onscroll", "ontouchstart",
            "ontouchend", "ontouchmove", "ontouchcancel", "onwheel" };

        [Fact]
        public void CleanTextShouldAcceptNullSearch()
        {
            var sut = new GdsValidation();
            var result = sut.CleanText(null, true, restrictedWords, allowedChars);
            result.Should().NotBe(null);
        }
        [Fact]
        public void CleanTextShouldAcceptNotNullSearch()
        {
            var search = "search";
            var sut = new GdsValidation();
            var result = sut.CleanText(search, true, restrictedWords, allowedChars);
            result.Should().Be(search);
        }
    }

}
