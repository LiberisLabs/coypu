using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenChecking
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Checks_an_unchecked_checkbox()
        {
            var checkbox = DriverSpecs.Field(_driver, "uncheckedBox"); 
            Assert.That(checkbox.Selected, Is.False);

            _driver.Check(checkbox);

            var findAgain = DriverSpecs.Field(_driver, "uncheckedBox");
            Assert.That(findAgain.Selected, Is.True);
        }

        [Test]
        public void Leaves_a_checked_checkbox_checked()
        {
            var checkbox = DriverSpecs.Field(_driver, "checkedBox");
            Assert.That(checkbox.Selected, Is.True);

            _driver.Check(checkbox);

            var findAgain = DriverSpecs.Field(_driver, "checkedBox");
            Assert.That(findAgain.Selected, Is.True);
        }

        [Test]
        public void Unchecks_a_checked_checkbox()
        {
            var checkbox = DriverSpecs.Field(_driver, "checkedBox");
            Assert.That(checkbox.Selected, Is.True);

            _driver.Uncheck(checkbox);

            var findAgain = DriverSpecs.Field(_driver, "checkedBox");
            Assert.That(findAgain.Selected, Is.False);
        }

        [Test]
        public void Leaves_an_unchecked_checkbox_unchecked()
        {
            var checkbox = DriverSpecs.Field(_driver, "uncheckedBox");
            Assert.That(checkbox.Selected, Is.False);

            _driver.Uncheck(checkbox);

            var findAgain = DriverSpecs.Field(_driver, "uncheckedBox");
            Assert.That(findAgain.Selected, Is.False);
        }

        [Test]
        public void Fires_onclick_event_on_check()
        {
            var checkbox = DriverSpecs.Field(_driver, "uncheckedBox");
            Assert.That(checkbox.Value, Is.EqualTo("unchecked"));

            _driver.Check(checkbox);

            Assert.That(DriverSpecs.Field(_driver, "uncheckedBox").Value, Is.EqualTo("unchecked - clicked"));
        }

        [Test]
        public void Fires_onclick_event_on_uncheck()
        {
            var checkbox = DriverSpecs.Field(_driver, "checkedBox");
            Assert.That(checkbox.Value, Is.EqualTo("checked"));

            _driver.Uncheck(checkbox);

            Assert.That(DriverSpecs.Field(_driver, "checkedBox").Value, Is.EqualTo("checked - clicked"));
        }
    }
}