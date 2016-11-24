using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAnElementByXpath
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_present_examples()
        {
            var shouldFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/span";
            Assert.That(DriverHelpers.XPath(_driver, shouldFind).Text, Is.EqualTo("This"));

            shouldFind = "//ul[@id='cssTest']/li[3]";
            Assert.That(DriverHelpers.XPath(_driver, shouldFind).Text, Is.EqualTo("Me! Pick me!"));
        }

        [Test]
        public void Does_not_find_missing_examples()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.XPath(_driver, shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }

        [Test]
        public void Only_finds_visible_elements()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/img";
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.XPath(_driver, shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }
    }
}