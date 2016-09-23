using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_all_elements_by_css : DriverSpecs
    {
        [Test]
        public void Returns_empty_if_no_matches()
        {
            const string shouldNotFind = "#inspectingContent p.css-missing-test";
            Assert.That(Driver.FindAllCss(shouldNotFind, Root, DefaultOptions), Is.Empty);
        }

        [Test]
        public void Returns_all_matches_by_css()
        {
            const string shouldFind = "#inspectingContent ul#cssTest li";
            var all = Driver.FindAllCss(shouldFind, Root, DefaultOptions);
            Assert.That(all.Count(), Is.EqualTo(3));
            Assert.That(all.ElementAt(1).Text, Is.EqualTo("two"));
            Assert.That(all.ElementAt(2).Text, Is.EqualTo("Me! Pick me!"));
        }
    }
}