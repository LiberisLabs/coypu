using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAllElementsByXpath
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Returns_empty_if_no_matches()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
            Assert.That(DriverSpecs.Driver.FindAllXPath(shouldNotFind, DriverSpecs.Root, DriverSpecs.DefaultOptions), Is.Empty);
        }

        [Test]
        public void Returns_all_matches_by_xpath()
        {
            const string shouldFind = "//*[@id='inspectingContent']//ul[@id='cssTest']/li";
            var all = DriverSpecs.Driver.FindAllXPath(shouldFind, DriverSpecs.Root, DriverSpecs.DefaultOptions);
            Assert.That(all.Count(), Is.EqualTo(3));
            Assert.That(all.ElementAt(1).Text, Is.EqualTo("two"));
            Assert.That(all.ElementAt(2).Text, Is.EqualTo("Me! Pick me!"));
        }
    }
}