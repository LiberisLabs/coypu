using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAnElementByCss
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_present_examples()
        {
            var shouldFind = "#inspectingContent p.css-test span";
            Assert.That(DriverHelpers.Css(DriverSpecs.Driver, shouldFind).Text, Is.EqualTo("This"));

            shouldFind = "ul#cssTest li:nth-child(3)";
            Assert.That(DriverHelpers.Css(DriverSpecs.Driver, shouldFind).Text, Is.EqualTo("Me! Pick me!"));
        }

        [Test]
        public void Finds_present_examples_by_text()
        {
            var shouldFind = "#inspectingContent p.css-test span";
            Assert.That(DriverHelpers.Css(DriverSpecs.Driver, shouldFind, new Regex("^This$")).Text, Is.EqualTo("This"));

            shouldFind = "ul#cssTest li:nth-child(3)";
            Assert.That(DriverHelpers.Css(DriverSpecs.Driver, shouldFind, new Regex("Pick me")).Text, Is.EqualTo("Me! Pick me!"));
        }

        [Test]
        public void Does_not_find_missing_examples()
        {
            const string shouldNotFind = "#inspectingContent p.css-missing-test";
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Css(DriverSpecs.Driver, shouldNotFind), "Expected not to find something at: " + shouldNotFind);
        }

        [Test]
        public void Does_not_find_missing_examples_by_text()
        {
            const string shouldFind = "ul#cssTest li:nth-child(3)";
            var missingTextPattern = new Regex("Drop me");
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Css(DriverSpecs.Driver, shouldFind, missingTextPattern),
                                                $"Expected not to find something at: {shouldFind} with text: {missingTextPattern}");
        }


        [Test]
        public void Only_finds_visible_elements()
        {
            const string shouldNotFind = "#inspectingContent p.css-test img.invisible";
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Css(DriverSpecs.Driver, shouldNotFind), "Expected not to find something at: " + shouldNotFind);
        }
    }
}