using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByContainerLabel
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
            Assert.That(DriverHelpers.Field(_driver, "text input field in a label container", options: Options.Exact).Id, Is.EqualTo("containerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(DriverHelpers.Field(_driver, "password field in a label container").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(_driver, "checkbox field in a label container").Id, Is.EqualTo("containerLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(DriverHelpers.Field(_driver, "radio field in a label container").Id, Is.EqualTo("containerLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(_driver, "select field in a label container").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(_driver, "textarea field in a label container").Id, Is.EqualTo("containerLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "file field in a label container").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring()
        {
            var fields = new FieldFinder(_driver, "Some container labeled radio option", _scope, Default.Options).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId",
                    "containerLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(_driver, "Some container labeled radio option", _scope, Default.Options).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId"
                }));
        }
    }
}