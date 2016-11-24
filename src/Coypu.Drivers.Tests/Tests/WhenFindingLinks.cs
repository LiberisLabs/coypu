using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingLinks
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_link_by_text()
        {
            Assert.That(DriverHelpers.Link(_driver, "first link").Id, Is.EqualTo("firstLinkId"));
            Assert.That(DriverHelpers.Link(_driver, "second link").Id, Is.EqualTo("secondLinkId"));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Link(_driver, "I am an invisible link by display"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Link(_driver, "I am an invisible link by visibility"));
        }

        [Test]
        public void Finds_a_link_with_both_types_of_quote_in_its_text()
        {
            Assert.That("linkWithBothQuotesId", Is.EqualTo(DriverHelpers.Link(_driver, "I'm a link with \"both\" types of quote in my text").Id));
        }
    }
}