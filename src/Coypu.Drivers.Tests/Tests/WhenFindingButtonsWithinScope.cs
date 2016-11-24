using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingButtonsWithinScope
    {
        private DriverScope _scope1;
        private DriverScope _scope2;
        private IDriver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [SetUp]
        public void SetUpScope()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            _scope1 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(_driver, "scope1", _scope, Default.Options), _driver, null, null, null, disambiguationStrategy);
            _scope2 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(_driver, "scope2", _scope, Default.Options), _driver, null, null, null, disambiguationStrategy);
        }

        [Test]
        public void Finds_button_by_name()
        {
            Assert.That(DriverHelpers.Button(_driver, "scopedButtonName", _scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "scopedButtonName", _scope2).Id, Is.EqualTo("scope2ButtonId"));
        }

        [Test]
        public void Finds_input_button_by_value()
        {
            Assert.That(DriverHelpers.Button(_driver, "scoped input button", _scope1).Id, Is.EqualTo("scope1InputButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "scoped input button", _scope2).Id, Is.EqualTo("scope2InputButtonId"));
        }

        [Test]
        public void Finds_button_by_text()
        {
            Assert.That(DriverHelpers.Button(_driver, "scoped button", _scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "scoped button", _scope2).Id, Is.EqualTo("scope2ButtonId"));
        }
    }
}