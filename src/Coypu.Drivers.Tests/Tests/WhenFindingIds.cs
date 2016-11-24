using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingIds 
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_element_by_id()
        {
            Assert.That(DriverHelpers.Id(DriverSpecs.Driver, "firstLinkId").Id, Is.EqualTo("firstLinkId"));
            Assert.That(DriverHelpers.Id(DriverSpecs.Driver, "secondLinkId").Id, Is.EqualTo("secondLinkId"));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(DriverSpecs.Driver, "invisibleLinkByDisplayId"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(DriverSpecs.Driver, "invisibleLinkByVisibilityId"));
        }

        [Test]
        public void Ignores_exact_option()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(DriverSpecs.Driver, "firstLink", null, Options.Substring));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(DriverSpecs.Driver, "firstLink", null, Options.Exact));
        }
    }
}