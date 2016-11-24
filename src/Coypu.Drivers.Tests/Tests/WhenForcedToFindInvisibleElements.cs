using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenForcedToFindInvisibleElements
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Does_find_hidden_inputs()
        {
            Assert.That("first hidden input", Is.EqualTo(DriverHelpers.Field(_driver, "firstHiddenInputId", options : Options.Invisible).Value));

            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Field(_driver, "firstHiddenInputId"));
        }

        [Test]
        public void Does_find_invisible_elements()
        {
            Assert.That("firstInvisibleInputName", Is.EqualTo(DriverHelpers.Button(_driver, "firstInvisibleInputId", options: Options.Invisible).Name));

            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstInvisibleInputId"));
        }

        [Test, Explicit("Only works in WatiN")]
        public void It_can_find_invisible_elements_by_text()
        {
            Assert.That(DriverHelpers.Css(_driver, "#firstInvisibleSpanId", new Regex("I am an invisible span"), null, Options.Invisible).Name,
                Is.EqualTo("firstInvisibleSpanName"));
        }

        [Test, Explicit("Only works this way in WebDriver")]
        public void Explains_it_cannot_find_invisible_elements_by_text()
        {
            Assert.Throws<NotSupportedException>(() =>
                DriverHelpers.Css(_driver, "#firstInvisibleSpanId", new Regex("I am an invisible span"), null, Options.Invisible));
        }
    }
}