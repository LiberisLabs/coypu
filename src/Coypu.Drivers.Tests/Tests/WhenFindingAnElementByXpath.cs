using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAnElementByXpath
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_present_examples()
        {
            var shouldFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/span";
            Assert.That("This", Is.EqualTo(DriverSpecs.XPath(shouldFind).Text));

            shouldFind = "//ul[@id='cssTest']/li[3]";
            Assert.That("Me! Pick me!", Is.EqualTo(DriverSpecs.XPath(shouldFind).Text));
        }

        [Test]
        public void Does_not_find_missing_examples()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.XPath(shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }

        [Test]
        public void Only_finds_visible_elements()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/img";
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.XPath(shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }
    }
}