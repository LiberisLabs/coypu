using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSelectingOptions
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        private static DriverScope GetSelectScope(string locator)
        {
            return new BrowserWindow(Default.SessionConfiguration,
                                     new SelectFinder(DriverSpecs.Driver, locator, DriverSpecs.Root, Default.Options), DriverSpecs.Driver,
                                     null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        [Test]
        public void Sets_text_of_selected_option()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledSelectFieldId").SelectedOption, Is.EqualTo("select two option one"));

            DriverSpecs.Driver.Click(DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), Default.Options)));

            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledSelectFieldId").SelectedOption, Is.EqualTo("select two option two"));
        }

        [Test]
        public void Selected_option_respects_TextPrecision()
        {
            Assert.That(
                DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Substring)).Text,
                Is.EqualTo("select two option two"));

            Assert.That(
                DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)).Text,
                Is.EqualTo("select two option two"));

            Assert.Throws<MissingHtmlException>(
                () => DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)));
        }

        [Test]
        public void Selected_option_finds_exact_by_container_label()
        {
            Assert.That("one", Is.EqualTo(DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "one", GetSelectScope("Ambiguous select options"), Options.Exact)).Text));
        }

        [Test]
        public void Selected_option_finds_substring_by_container_label()
        {
            Assert.That("one", Is.EqualTo(DriverHelpers.FindSingle(new OptionFinder(DriverSpecs.Driver, "one", GetSelectScope("Ambiguous select options"), Options.Substring)).Text));
        }
    }
}