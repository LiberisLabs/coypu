using Coypu.Finders;
using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class WhenFindingButtonsWithinScope : DriverSpecs
    {
        private DriverScope _scope1;
        private DriverScope _scope2;

        [SetUp]
        public void SetUpScope()
        {
            _scope1 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope1", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
            _scope2 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope2", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
        }

        [Test]
        public void Finds_button_by_name()
        {
            Button("scopedButtonName", _scope1).Id.should_be("scope1ButtonId");
            Button("scopedButtonName", _scope2).Id.should_be("scope2ButtonId");
        }

        [Test]
        public void Finds_input_button_by_value()
        {
            Button("scoped input button", _scope1).Id.should_be("scope1InputButtonId");
            Button("scoped input button", _scope2).Id.should_be("scope2InputButtonId");
        }

        [Test]
        public void Finds_button_by_text()
        {
            Button("scoped button", _scope1).Id.should_be("scope1ButtonId");
            Button("scoped button", _scope2).Id.should_be("scope2ButtonId");
        }
    }
}