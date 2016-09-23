using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_finding_ids : DriverSpecs
    {
        [Test]
        public void Finds_element_by_id()
        {
            Assert.That(Id("firstLinkId").Id, Is.EqualTo("firstLinkId"));
            Assert.That(Id("secondLinkId").Id, Is.EqualTo("secondLinkId"));
        }

        [Test]
        public void Does_not_find_display_none()
        {
            Assert.Throws<MissingHtmlException>(() => Id("invisibleLinkByDisplayId"));
        }

        [Test]
        public void Does_not_find_visibility_hidden_links()
        {
            Assert.Throws<MissingHtmlException>(() => Id("invisibleLinkByVisibilityId"));
        }

        [Test]
        public void Ignores_exact_option()
        {
            Assert.Throws<MissingHtmlException>(() => Id("firstLink", options: Options.Substring));
            Assert.Throws<MissingHtmlException>(() => Id("firstLink", options: Options.Exact));
        }
    }
}