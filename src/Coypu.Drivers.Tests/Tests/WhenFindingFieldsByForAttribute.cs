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
            Assert.That("forLabeledTextInputFieldId", Is.EqualTo(DriverSpecs.Field("text input field linked by for", options: Options.Exact).Id));
        }

        [Test]
        public void Finds_password_field()
        {
            Assert.That("forLabeledPasswordFieldId", Is.EqualTo(DriverSpecs.Field("password field linked by for").Id));
        }

        [Test]
        public void Finds_select_field()
        {
            Assert.That("forLabeledSelectFieldId", Is.EqualTo(DriverSpecs.Field("select field linked by for").Id));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That("forLabeledCheckboxFieldId", Is.EqualTo(DriverSpecs.Field("checkbox field linked by for").Id));
        }

        [Test]
        public void Finds_radio_button()
        {
            Assert.That("forLabeledRadioFieldId", Is.EqualTo(DriverSpecs.Field("radio field linked by for").Id));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That("forLabeledTextareaFieldId", Is.EqualTo(DriverSpecs.Field("textarea field linked by for").Id));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That("forLabeledFileFieldId", Is.EqualTo(DriverSpecs.Field("file field linked by for").Id));
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