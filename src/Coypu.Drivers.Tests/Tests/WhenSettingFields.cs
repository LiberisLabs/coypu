using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSettingFields
    {
        private IDriver _driver;

        [SetUp]
        public void Given() => _driver = TestDriver.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        private static DriverScope GetSelectScope(IDriver driver, string locator)
        {
            return new BrowserWindow(Default.SessionConfiguration,
                                     new SelectFinder(driver, locator, DriverHelpers.WindowScope(driver), Default.Options), driver,
                                     null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        [Test]
        public void Sets_value_of_text_input_field_with_id()
        {
            var textField = DriverHelpers.Field(_driver, "containerLabeledTextInputFieldName");
            _driver.Set(textField, "should be much quicker since it's set by js");

            Assert.That(textField.Value, Is.EqualTo("should be much quicker since it's set by js"));

            var findAgain = DriverHelpers.Field(_driver, "containerLabeledTextInputFieldName");
            Assert.That(findAgain.Value, Is.EqualTo("should be much quicker since it's set by js"));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_id()
        {
            var textField = DriverHelpers.Field(_driver, "Field with no id");
            _driver.Set(textField, "set by sendkeys");

            Assert.That(textField.Value, Is.EqualTo("set by sendkeys"));

            var findAgain = DriverHelpers.Field(_driver, "Field with no id");
            Assert.That(findAgain.Value, Is.EqualTo("set by sendkeys"));
        }

        [Test]
        public void Sets_value_of_number_input_field()
        {
            var numberField = DriverHelpers.Field(_driver, "containerLabeledNumberInputFieldId");
            _driver.Set(numberField, "5150");

            Assert.That(numberField.Value, Is.EqualTo("5150"));

            var findAgain = DriverHelpers.Field(_driver, "containerLabeledNumberInputFieldId");
            Assert.That(findAgain.Value, Is.EqualTo("5150"));
        }

        [Test]
        public void Sets_value_of_text_input_field_with_no_type()
        {
            var textField = DriverHelpers.Field(_driver, "fieldWithNoType");
            _driver.Set(textField, "set by sendkeys");

            Assert.That(textField.Value, Is.EqualTo("set by sendkeys"));

            var findAgain = DriverHelpers.Field(_driver, "fieldWithNoType");
            Assert.That(findAgain.Value, Is.EqualTo("set by sendkeys"));
        }

        [Test]
        public void Sets_value_of_textarea_field()
        {
            var textField = DriverHelpers.Field(_driver, "containerLabeledTextareaFieldName");
            _driver.Set(textField, "New textarea value");

            Assert.That(textField.Value, Is.EqualTo("New textarea value"));

            var findAgain = DriverHelpers.Field(_driver, "containerLabeledTextareaFieldName");
            Assert.That(findAgain.Value, Is.EqualTo("New textarea value"));
        }

        [Test]
        public void Selects_option_by_text_or_value()
        {
            var textField = DriverHelpers.Field(_driver, "containerLabeledSelectFieldId");
            Assert.That(textField.Value, Is.EqualTo("select2value1"));

            _driver.Click(DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option two", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Default.Options)));

            var findAgain = DriverHelpers.Field(_driver, "containerLabeledSelectFieldId");
            Assert.That(findAgain.Value, Is.EqualTo("select2value2"));

            _driver.Click(DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option one", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Default.Options)));

            var andAgain = DriverHelpers.Field(_driver, "containerLabeledSelectFieldId");
            Assert.That(andAgain.Value, Is.EqualTo("select2value1"));
        }

        [Test]
        public void Fires_change_event_when_selecting_an_option()
        {
            var textField = DriverHelpers.Field(_driver, "containerLabeledSelectFieldId");
            Assert.That(textField.Name, Is.EqualTo("containerLabeledSelectFieldName"));

            _driver.Click(DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option two", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Default.Options)));

            Assert.That(DriverHelpers.Field(_driver, "containerLabeledSelectFieldId", DriverHelpers.WindowScope(_driver)).Name, Is.EqualTo("containerLabeledSelectFieldName - changed"));
        }
    }
}