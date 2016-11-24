using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenExecutingScript
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = TestDriver.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Runs_the_script_in_the_browser()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstButtonId").Text, Is.EqualTo("first button"));

            _driver.ExecuteScript("document.getElementById('firstButtonId').innerHTML = 'script executed';", DriverHelpers.WindowScope(_driver));

            Assert.That(DriverHelpers.Button(_driver, "firstButtonId").Text, Is.EqualTo("script executed"));
        }

        [Test]
        public void Passes_the_arguments_to_the_browser()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstButtonId").Text, Is.EqualTo("first button"));

            _driver.ExecuteScript("arguments[0].innerHTML = 'script executed ' + arguments[1];", DriverHelpers.WindowScope(_driver), DriverHelpers.Button(_driver, "firstButtonId"), 5);

            Assert.That(DriverHelpers.Button(_driver, "firstButtonId").Text, Is.EqualTo("script executed 5"));
        }
      
        [Test]
        public void Returns_the_result()
        {
            Assert.That(_driver.ExecuteScript("return document.getElementById('firstButtonId').innerHTML;", DriverHelpers.WindowScope(_driver)), Is.EqualTo("first button"));
        }
    }
}