using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_finding_buttons_within_scope : DriverSpecs
    {
        private DriverScope scope1;
        private DriverScope scope2;

        [SetUp]
        public void SetUpScope()
        {
            scope1 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope1", Root, DefaultOptions), Driver,null,null,null,DisambiguationStrategy);
            scope2 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope2", Root, DefaultOptions), Driver,null,null,null,DisambiguationStrategy);
        }

        [Test]
        public void Finds_button_by_name()
        {
            Assert.That(Button("scopedButtonName", scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(Button( "scopedButtonName", scope2).Id, Is.EqualTo("scope2ButtonId"));
        }

        [Test]
        public void Finds_input_button_by_value()
        {
            Assert.That(Button( "scoped input button", scope1).Id, Is.EqualTo("scope1InputButtonId"));
            Assert.That(Button( "scoped input button", scope2).Id, Is.EqualTo("scope2InputButtonId"));
        }

        [Test]
        public void Finds_button_by_text()
        {
            Assert.That(Button( "scoped button", scope1).Id, Is.EqualTo("scope1ButtonId"));
            Assert.That(Button( "scoped button", scope2).Id, Is.EqualTo("scope2ButtonId"));
        }
    }
}