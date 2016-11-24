using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenForcedToFindInvisibleElements
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Does_find_hidden_inputs()
        {
            Assert.That("first hidden input", Is.EqualTo(DriverSpecs.Field("firstHiddenInputId", options : Options.Invisible).Value));

            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Field("firstHiddenInputId"));
        }

        [Test]
        public void Does_find_invisible_elements()
        {
            Assert.That("firstInvisibleInputName", Is.EqualTo(DriverSpecs.Button("firstInvisibleInputId", options: Options.Invisible).Name));

            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstInvisibleInputId"));
        }

        [Test, Explicit("Only works in WatiN")]
        public void It_can_find_invisible_elements_by_text()
        {
            Assert.That(DriverSpecs.Css("#firstInvisibleSpanId",new Regex("I am an invisible span"), options: Options.Invisible).Name,
                Is.EqualTo("firstInvisibleSpanName"));
        }

        [Test, Explicit("Only works this way in WebDriver")]
        public void Explains_it_cannot_find_invisible_elements_by_text()
        {
            Assert.Throws<NotSupportedException>(() =>
                DriverSpecs.Css("#firstInvisibleSpanId", new Regex("I am an invisible span"), options: Options.Invisible));
        }
    }
}