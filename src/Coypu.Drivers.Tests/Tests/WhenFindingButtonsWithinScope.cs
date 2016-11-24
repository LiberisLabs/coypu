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
        public void Given() => DriverSpecs.DoSetUp();

        [SetUp]
        public void SetUpScope()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            _scope1 = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope1", DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
            _scope2 = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope2", DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
        }

        [Test]
        public void Finds_button_by_name()
        {
            Assert.That("scope1ButtonId", Is.EqualTo(DriverSpecs.Button("scopedButtonName", _scope1).Id));
            Assert.That("scope2ButtonId", Is.EqualTo(DriverSpecs.Button("scopedButtonName", _scope2).Id));
        }

        [Test]
        public void Finds_input_button_by_value()
        {
            Assert.That("scope1InputButtonId", Is.EqualTo(DriverSpecs.Button("scoped input button", _scope1).Id));
            Assert.That("scope2InputButtonId", Is.EqualTo(DriverSpecs.Button("scoped input button", _scope2).Id));
        }

        [Test]
        public void Finds_button_by_text()
        {
            Assert.That("scope1ButtonId", Is.EqualTo(DriverSpecs.Button("scoped button", _scope1).Id));
            Assert.That("scope2ButtonId", Is.EqualTo(DriverSpecs.Button("scoped button", _scope2).Id));
        }
    }
}