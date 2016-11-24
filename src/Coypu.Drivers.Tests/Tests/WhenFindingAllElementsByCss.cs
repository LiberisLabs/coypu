using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingAllElementsByCss
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Returns_empty_if_no_matches()
        {
            const string shouldNotFind = "#inspectingContent p.css-missing-test";
            Assert.That(DriverSpecs.Driver.FindAllCss(shouldNotFind, DriverSpecs.Root, DriverSpecs.DefaultOptions), Is.Empty);
        }

        [Test]
        public void Returns_all_matches_by_css()
        {
            const string shouldFind = "#inspectingContent ul#cssTest li";
            var all = DriverSpecs.Driver.FindAllCss(shouldFind, DriverSpecs.Root, DriverSpecs.DefaultOptions);
            Assert.That(all.Count(), Is.EqualTo(3));
            Assert.That(all.ElementAt(1).Text, Is.EqualTo("two"));
            Assert.That(all.ElementAt(2).Text, Is.EqualTo("Me! Pick me!"));
        }
    }
}