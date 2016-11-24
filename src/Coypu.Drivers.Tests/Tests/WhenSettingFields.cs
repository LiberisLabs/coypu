using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSettingFields
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        private static DriverScope GetSelectScope(string locator)
        {
            return new BrowserWindow(DriverSpecs.DefaultSessionConfiguration,
                                     new SelectFinder(DriverSpecs.Driver, locator, DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver,
                                     null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = DriverSpecs.Field("containerLabeledTextInputFieldName");
            DriverSpecs.Driver.Set(textField, "should be much quicker since it's set by js");

            Assert.That("should be much quicker since it's set by js", Is.EqualTo(textField.Value));

            var findAgain = DriverSpecs.Field("containerLabeledTextInputFieldName");
            Assert.That("should be much quicker since it's set by js", Is.EqualTo(findAgain.Value));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_id()
        {
            var textField = DriverSpecs.Field("Field with no id");
            DriverSpecs.Driver.Set(textField, "set by sendkeys");

            Assert.That("set by sendkeys", Is.EqualTo(textField.Value));

            var findAgain = DriverSpecs.Field("Field with no id");
            Assert.That("set by sendkeys", Is.EqualTo(findAgain.Value));
        }

        [Test]
        public void Sets_value_of_number_input_field()
        {
            var numberField = DriverSpecs.Field("containerLabeledNumberInputFieldId");
            DriverSpecs.Driver.Set(numberField, "5150");

            Assert.That("5150", Is.EqualTo(numberField.Value));

            var findAgain = DriverSpecs.Field("containerLabeledNumberInputFieldId");
            Assert.That("5150", Is.EqualTo(findAgain.Value));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_type()
        {
            var textField = DriverSpecs.Field("fieldWithNoType");
            DriverSpecs.Driver.Set(textField, "set by sendkeys");

            Assert.That("set by sendkeys", Is.EqualTo(textField.Value));

            var findAgain = DriverSpecs.Field("fieldWithNoType");
            Assert.That("set by sendkeys", Is.EqualTo(findAgain.Value));
        }

        [Test]
        public void Sets_value_of_textarea_field()
        {
            var textField = DriverSpecs.Field("containerLabeledTextareaFieldName");
            DriverSpecs.Driver.Set(textField, "New textarea value");

            Assert.That("New textarea value", Is.EqualTo(textField.Value));

            var findAgain = DriverSpecs.Field("containerLabeledTextareaFieldName");
            Assert.That("New textarea value", Is.EqualTo(findAgain.Value));
        }

        [Test]
        public void Selects_option_by_text_or_value()
        {
            var textField = DriverSpecs.Field("containerLabeledSelectFieldId");
            Assert.That("select2value1", Is.EqualTo(textField.Value));

            DriverSpecs.Driver.Click(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DriverSpecs.DefaultOptions)));

            var findAgain = DriverSpecs.Field("containerLabeledSelectFieldId");
            Assert.That("select2value2", Is.EqualTo(findAgain.Value));

            DriverSpecs.Driver.Click(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option one", GetSelectScope("containerLabeledSelectFieldId"), DriverSpecs.DefaultOptions)));

            var andAgain = DriverSpecs.Field("containerLabeledSelectFieldId");
            Assert.That("select2value1", Is.EqualTo(andAgain.Value));
        }

        [Test]
        public void Fires_change_event_when_selecting_an_option()
        {
            var textField = DriverSpecs.Field("containerLabeledSelectFieldId");
            Assert.That("containerLabeledSelectFieldName", Is.EqualTo(textField.Name));

            DriverSpecs.Driver.Click(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DriverSpecs.DefaultOptions)));

            Assert.That("containerLabeledSelectFieldName - changed", Is.EqualTo(DriverSpecs.Field("containerLabeledSelectFieldId", DriverSpecs.Root).Name));
        }
    }
}