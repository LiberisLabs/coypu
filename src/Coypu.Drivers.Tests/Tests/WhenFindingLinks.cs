using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingLinks
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_link_by_text()
        {
            Assert.That(DriverSpecs.Link("first link").Id, Is.EqualTo("firstLinkId"));
            Assert.That(DriverSpecs.Link("second link").Id, Is.EqualTo("secondLinkId"));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Link("I am an invisible link by display"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Link("I am an invisible link by visibility"));
        }

        [Test]
        public void Finds_a_link_with_both_types_of_quote_in_its_text()
        {
            Assert.That("linkWithBothQuotesId", Is.EqualTo(DriverSpecs.Link("I'm a link with \"both\" types of quote in my text").Id));
        }
    }
}