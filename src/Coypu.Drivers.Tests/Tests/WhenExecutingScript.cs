using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenExecutingScript
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Runs_the_script_in_the_browser()
        {
            Assert.That("first button", Is.EqualTo(DriverSpecs.Button(_driver, "firstButtonId").Text));

            _driver.ExecuteScript("document.getElementById('firstButtonId').innerHTML = 'script executed';", DriverSpecs.Root);

            Assert.That("script executed", Is.EqualTo(DriverSpecs.Button(_driver, "firstButtonId").Text));
        }

        [Test]
        public void Passes_the_arguments_to_the_browser()
        {
            Assert.That("first button", Is.EqualTo(DriverSpecs.Button("firstButtonId").Text));

            _driver.ExecuteScript("arguments[0].innerHTML = 'script executed ' + arguments[1];", DriverSpecs.Root, DriverSpecs.Button("firstButtonId"), 5);

            Assert.That("script executed 5", Is.EqualTo(DriverSpecs.Button(_driver, "firstButtonId").Text));
        }
      
        [Test]
        public void Returns_the_result()
        {
            Assert.That("first button", Is.EqualTo(_driver.ExecuteScript("return document.getElementById('firstButtonId').innerHTML;", DriverSpecs.Root)));
        }
    }
}