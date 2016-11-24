using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByForAttribute
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "text input field linked by for", options: Options.Exact).Id, Is.EqualTo("forLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "password field linked by for").Id, Is.EqualTo("forLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_select_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "select field linked by for").Id, Is.EqualTo("forLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "checkbox field linked by for").Id, Is.EqualTo("forLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio_button()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "radio field linked by for").Id, Is.EqualTo("forLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "textarea field linked by for").Id, Is.EqualTo("forLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "file field linked by for").Id, Is.EqualTo("forLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring_text()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some for labeled radio option", DriverSpecs.Root, Default.Options).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId",
                    "forLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some for labeled radio option", DriverSpecs.Root, Default.Options).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId"
                }));
        }
    }
}