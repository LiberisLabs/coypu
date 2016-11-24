using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSendingKeysToElements
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = DriverSpecs.Field("containerLabeledTextInputFieldName");
            Assert.That(textField.Value, Is.EqualTo("text input field two val"));

            DriverSpecs.Driver.SendKeys(textField, " - send keys without any click, clear first, etc");

            Assert.That(textField.Value, Is.EqualTo("text input field two val - send keys without any click, clear first, etc"));
        }
    }
}