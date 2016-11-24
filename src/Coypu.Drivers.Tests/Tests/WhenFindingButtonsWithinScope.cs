using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingButtonsWithinScope
    {
        private DriverScope _scope1;
        private DriverScope _scope2;

        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [SetUp]
        public void SetUpScope()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            _scope1 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope1", DriverSpecs.Root, Default.Options), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
            _scope2 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope2", DriverSpecs.Root, Default.Options), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
        }

        [Test]
        public void Finds_button_by_name()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scopedButtonName", _scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scopedButtonName", _scope2).Id, Is.EqualTo("scope2ButtonId"));
        }

        [Test]
        public void Finds_input_button_by_value()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scoped input button", _scope1).Id, Is.EqualTo("scope1InputButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scoped input button", _scope2).Id, Is.EqualTo("scope2InputButtonId"));
        }

        [Test]
        public void Finds_button_by_text()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scoped button", _scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "scoped button", _scope2).Id, Is.EqualTo("scope2ButtonId"));
        }
    }
}