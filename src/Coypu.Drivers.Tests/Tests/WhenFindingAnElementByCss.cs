using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAnElementByCss
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_present_examples()
        {
            var shouldFind = "#inspectingContent p.css-test span";
            Assert.That("This", Is.EqualTo(DriverSpecs.Css(shouldFind).Text));

            shouldFind = "ul#cssTest li:nth-child(3)";
            Assert.That("Me! Pick me!", Is.EqualTo(DriverSpecs.Css(shouldFind).Text));
        }

        [Test]
        public void Finds_present_examples_by_text()
        {
            var shouldFind = "#inspectingContent p.css-test span";
            Assert.That("This", Is.EqualTo(DriverSpecs.Css(shouldFind, new Regex("^This$")).Text));

            shouldFind = "ul#cssTest li:nth-child(3)";
            Assert.That("Me! Pick me!", Is.EqualTo(DriverSpecs.Css(shouldFind, new Regex("Pick me")).Text));
        }

        [Test]
        public void Does_not_find_missing_examples()
        {
            const string shouldNotFind = "#inspectingContent p.css-missing-test";
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Css(shouldNotFind, DriverSpecs.Root), "Expected not to find something at: " + shouldNotFind);
        }

        [Test]
        public void Does_not_find_missing_examples_by_text()
        {
            const string shouldFind = "ul#cssTest li:nth-child(3)";
            var missingTextPattern = new Regex("Drop me");
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Css(shouldFind, missingTextPattern),
                                                string.Format("Expected not to find something at: {0} with text: ", shouldFind, missingTextPattern));
        }


        [Test]
        public void Only_finds_visible_elements()
        {
            const string shouldNotFind = "#inspectingContent p.css-test img.invisible";
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Css(shouldNotFind, DriverSpecs.Root), "Expected not to find something at: " + shouldNotFind);
        }
    }
}