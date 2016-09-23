using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_setting_fields : DriverSpecs
    {

        private static DriverScope GetSelectScope(string locator)
        {
            var select = new BrowserWindow(DefaultSessionConfiguration,
                                         new SelectFinder(Driver, locator, Root, DefaultOptions), Driver,
                                         null, null, null, DisambiguationStrategy);
            return @select;
        }

        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = Field("containerLabeledTextInputFieldName");  
            Driver.Set(textField, "should be much quicker since it's set by js");

            Assert.That(textField.Value, Is.EqualTo("should be much quicker since it's set by js"));

            var findAgain = Field("containerLabeledTextInputFieldName");
            Assert.That(findAgain.Value, Is.EqualTo("should be much quicker since it's set by js"));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_id()
        {
            var textField = Field("Field with no id");
            Driver.Set(textField, "set by sendkeys");

            Assert.That(textField.Value, Is.EqualTo("set by sendkeys"));

            var findAgain = Field("Field with no id");
            Assert.That(findAgain.Value, Is.EqualTo("set by sendkeys"));
        }

        [Test]
        public void Sets_value_of_number_input_field()
        {
            var numberField = Field("containerLabeledNumberInputFieldId");
            Driver.Set(numberField, "5150");

            Assert.That(numberField.Value, Is.EqualTo("5150"));

            var findAgain = Field("containerLabeledNumberInputFieldId");
            Assert.That(findAgain.Value, Is.EqualTo("5150"));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_type()
        {
            var textField = Field("fieldWithNoType");
            Driver.Set(textField, "set by sendkeys");

            Assert.That(textField.Value, Is.EqualTo("set by sendkeys"));

            var findAgain = Field("fieldWithNoType");
            Assert.That(findAgain.Value, Is.EqualTo("set by sendkeys"));
        }


        [Test]
        public void Sets_value_of_textarea_field()
        {
            var textField = Field("containerLabeledTextareaFieldName");
            Driver.Set(textField, "New textarea value");

            Assert.That(textField.Value, Is.EqualTo("New textarea value"));

            var findAgain = Field("containerLabeledTextareaFieldName");
            Assert.That(findAgain.Value, Is.EqualTo("New textarea value"));
        }


        [Test]
        public void Selects_option_by_text_or_value()
        {
            var textField = Field("containerLabeledSelectFieldId");
            Assert.That(textField.Value, Is.EqualTo("select2value1"));

            Driver.Click(FindSingle(new OptionFinder(Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DefaultOptions)));

            var findAgain = Field("containerLabeledSelectFieldId");
            Assert.That(findAgain.Value, Is.EqualTo("select2value2"));

            Driver.Click(FindSingle(new OptionFinder(Driver, "select two option one", GetSelectScope("containerLabeledSelectFieldId"), DefaultOptions)));

            var andAgain = Field("containerLabeledSelectFieldId");
            Assert.That(andAgain.Value, Is.EqualTo("select2value1"));
        }

        [Test]
        public void Fires_change_event_when_selecting_an_option()
        {
            var textField = Field("containerLabeledSelectFieldId");
            Assert.That(textField.Name, Is.EqualTo("containerLabeledSelectFieldName"));

            Driver.Click(FindSingle(new OptionFinder(Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DefaultOptions)));

            Assert.That(Field("containerLabeledSelectFieldId", Root).Name, Is.EqualTo("containerLabeledSelectFieldName - changed"));
        }
    }
}