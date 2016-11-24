using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByForAttribute
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverHelpers.Field(_driver, "text input field linked by for", options: Options.Exact).Id, Is.EqualTo("forLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "password field linked by for").Id, Is.EqualTo("forLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_select_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "select field linked by for").Id, Is.EqualTo("forLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(_driver, "checkbox field linked by for").Id, Is.EqualTo("forLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio_button()
        {
            Assert.That(DriverHelpers.Field(_driver, "radio field linked by for").Id, Is.EqualTo("forLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(_driver, "textarea field linked by for").Id, Is.EqualTo("forLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverHelpers.Field(_driver, "file field linked by for").Id, Is.EqualTo("forLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring_text()
        {
            var fields = new FieldFinder(_driver, "Some for labeled radio option", _scope, Default.Options).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId",
                    "forLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(_driver, "Some for labeled radio option", _scope, Default.Options).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId"
                }));
        }
    }
}