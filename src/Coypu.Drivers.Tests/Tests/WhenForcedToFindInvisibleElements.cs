using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenForcedToFindInvisibleElements
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Does_find_hidden_inputs()
        {
            Assert.That("first hidden input", Is.EqualTo(DriverHelpers.Field(DriverSpecs.Driver, "firstHiddenInputId", options : Options.Invisible).Value));

            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Field(DriverSpecs.Driver, "firstHiddenInputId"));
        }

        [Test]
        public void Does_find_invisible_elements()
        {
            Assert.That("firstInvisibleInputName", Is.EqualTo(DriverHelpers.Button(DriverSpecs.Driver, "firstInvisibleInputId", options: Options.Invisible).Name));

            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstInvisibleInputId"));
        }

        [Test, Explicit("Only works in WatiN")]
        public void It_can_find_invisible_elements_by_text()
        {
            Assert.That(DriverHelpers.Css(DriverSpecs.Driver, "#firstInvisibleSpanId", new Regex("I am an invisible span"), null, Options.Invisible).Name,
                Is.EqualTo("firstInvisibleSpanName"));
        }

        [Test, Explicit("Only works this way in WebDriver")]
        public void Explains_it_cannot_find_invisible_elements_by_text()
        {
            Assert.Throws<NotSupportedException>(() =>
                DriverHelpers.Css(DriverSpecs.Driver, "#firstInvisibleSpanId", new Regex("I am an invisible span"), null, Options.Invisible));
        }
    }
}