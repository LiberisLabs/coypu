using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSelectingOptions
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        private static DriverScope GetSelectScope(Driver driver, string locator)
        {
            return new BrowserWindow(Default.SessionConfiguration,
                                     new SelectFinder(driver, locator, DriverHelpers.WindowScope(driver), Default.Options), driver,
                                     null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        [Test]
        public void Sets_text_of_selected_option()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledSelectFieldId").SelectedOption, Is.EqualTo("select two option one"));

            _driver.Click(DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option two", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Default.Options)));

            Assert.That(DriverHelpers.Field(_driver, "containerLabeledSelectFieldId").SelectedOption, Is.EqualTo("select two option two"));
        }

        [Test]
        public void Selected_option_respects_TextPrecision()
        {
            Assert.That(
                DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option t", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Options.Substring)).Text,
                Is.EqualTo("select two option two"));

            Assert.That(
                DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option two", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Options.Exact)).Text,
                Is.EqualTo("select two option two"));

            Assert.Throws<MissingHtmlException>(
                () => DriverHelpers.FindSingle(new OptionFinder(_driver, "select two option t", GetSelectScope(_driver, "containerLabeledSelectFieldId"), Options.Exact)));
        }

        [Test]
        public void Selected_option_finds_exact_by_container_label()
        {
            Assert.That("one", Is.EqualTo(DriverHelpers.FindSingle(new OptionFinder(_driver, "one", GetSelectScope(_driver, "Ambiguous select options"), Options.Exact)).Text));
        }

        [Test]
        public void Selected_option_finds_substring_by_container_label()
        {
            Assert.That("one", Is.EqualTo(DriverHelpers.FindSingle(new OptionFinder(_driver, "one", GetSelectScope(_driver, "Ambiguous select options"), Options.Substring)).Text));
        }
    }
}