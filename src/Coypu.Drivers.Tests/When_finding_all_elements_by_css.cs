using System.Linq;
using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenFindingAllElementsByCss : DriverSpecs
    {
        private TestDriver _driver;

        [OneTimeSetUp]
        public void GivenATestDriver()
        {
            _driver = TestDriver.Create();
        }

        [Test]
        public void Returns_empty_if_no_matches()
        {
            const string shouldNotFind = "#inspectingContent p.css-missing-test";
            Assert.That(_driver.Driver.FindAllCss(shouldNotFind, Root, DefaultOptions), Is.Empty);
        }

        [Test]
        public void Returns_all_matches_by_css()
        {
            const string shouldFind = "#inspectingContent ul#cssTest li";
            var all = _driver.Driver.FindAllCss(shouldFind, Root, DefaultOptions);
            all.Count().should_be(3);
            all.ElementAt(1).Text.should_be("two");
            all.ElementAt(2).Text.should_be("Me! Pick me!");
        }
    }
}