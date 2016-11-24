using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAllElementsByXpath
    {
        private IDriver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [Test]
        public void Returns_empty_if_no_matches()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
            Assert.That(_driver.FindAllXPath(shouldNotFind, _scope, Default.Options), Is.Empty);
        }

        [Test]
        public void Returns_all_matches_by_xpath()
        {
            const string shouldFind = "//*[@id='inspectingContent']//ul[@id='cssTest']/li";
            var all = _driver.FindAllXPath(shouldFind, _scope, Default.Options);
            Assert.That(all.Count(), Is.EqualTo(3));
            Assert.That(all.ElementAt(1).Text, Is.EqualTo("two"));
            Assert.That(all.ElementAt(2).Text, Is.EqualTo("Me! Pick me!"));
        }
    }
}