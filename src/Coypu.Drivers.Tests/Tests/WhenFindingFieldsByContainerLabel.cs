using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByContainerLabel
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "text input field in a label container", options: Options.Exact).Id, Is.EqualTo("containerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "password field in a label container").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "checkbox field in a label container").Id, Is.EqualTo("containerLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "radio field in a label container").Id, Is.EqualTo("containerLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "select field in a label container").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "textarea field in a label container").Id, Is.EqualTo("containerLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "file field in a label container").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some container labeled radio option", DriverSpecs.Root, Default.Options).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId",
                    "containerLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some container labeled radio option", DriverSpecs.Root, Default.Options).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId"
                }));
        }
    }
}