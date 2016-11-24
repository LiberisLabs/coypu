using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByForAttribute
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverSpecs.Field("text input field linked by for", options: Options.Exact).Id, Is.EqualTo("forLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password_field()
        {
            Assert.That(DriverSpecs.Field("password field linked by for").Id, Is.EqualTo("forLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_select_field()
        {
            Assert.That(DriverSpecs.Field("select field linked by for").Id, Is.EqualTo("forLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverSpecs.Field("checkbox field linked by for").Id, Is.EqualTo("forLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio_button()
        {
            Assert.That(DriverSpecs.Field("radio field linked by for").Id, Is.EqualTo("forLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverSpecs.Field("textarea field linked by for").Id, Is.EqualTo("forLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverSpecs.Field("file field linked by for").Id, Is.EqualTo("forLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring_text()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some for labeled radio option", DriverSpecs.Root, DriverSpecs.DefaultOptions).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId",
                    "forLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(DriverSpecs.Driver, "Some for labeled radio option", DriverSpecs.Root, DriverSpecs.DefaultOptions).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId"
                }));
        }
    }
}