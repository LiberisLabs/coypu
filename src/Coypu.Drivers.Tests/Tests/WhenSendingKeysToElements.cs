using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSendingKeysToElements
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = DriverHelpers.Field(_driver, "containerLabeledTextInputFieldName");
            Assert.That(textField.Value, Is.EqualTo("text input field two val"));

            _driver.SendKeys(textField, " - send keys without any click, clear first, etc");

            Assert.That(textField.Value, Is.EqualTo("text input field two val - send keys without any click, clear first, etc"));
        }
    }
}