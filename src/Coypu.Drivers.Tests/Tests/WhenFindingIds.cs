using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingIds 
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_element_by_id()
        {
            Assert.That("firstLinkId", Is.EqualTo(DriverSpecs.Id("firstLinkId").Id));
            Assert.That("secondLinkId", Is.EqualTo(DriverSpecs.Id("secondLinkId").Id));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Id("invisibleLinkByDisplayId"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Id("invisibleLinkByVisibilityId"));
        }

        [Test]
        public void Ignores_exact_option()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Id("firstLink", options: Options.Substring));
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Id("firstLink", options: Options.Exact));
        }
    }
}