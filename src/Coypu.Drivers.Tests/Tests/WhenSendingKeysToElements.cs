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
            Assert.That("text input field two val", Is.EqualTo(textField.Value));

            DriverSpecs.Driver.SendKeys(textField, " - send keys without any click, clear first, etc");

            Assert.That("text input field two val - send keys without any click, clear first, etc", Is.EqualTo(textField.Value));
        }
    }
}