﻿using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_sending_keys_to_elements : DriverSpecs
    {
        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = Field("containerLabeledTextInputFieldName");
            Assert.That(textField.Value, Is.EqualTo("text input field two val"));

            Driver.SendKeys(textField, " - send keys without any click, clear first, etc");

            Assert.That(textField.Value, Is.EqualTo("text input field two val - send keys without any click, clear first, etc"));
        }
    }
}