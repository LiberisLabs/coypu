using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingIds 
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_element_by_id()
        {
            Assert.That(DriverHelpers.Id(_driver, "firstLinkId").Id, Is.EqualTo("firstLinkId"));
            Assert.That(DriverHelpers.Id(_driver, "secondLinkId").Id, Is.EqualTo("secondLinkId"));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(_driver, "invisibleLinkByDisplayId"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(_driver, "invisibleLinkByVisibilityId"));
        }

        [Test]
        public void Ignores_exact_option()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(_driver, "firstLink", null, Options.Substring));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Id(_driver, "firstLink", null, Options.Exact));
        }
    }
}