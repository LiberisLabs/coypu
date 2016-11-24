using System.Drawing;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSizingWindows
    {
        private static class Helpers
        {
            public static void AssertMaximisesWindow(Driver driver, Scope driverScope)
            {
                var availWidth = driver.ExecuteScript("return window.screen.availWidth;", driverScope);
                var initalWidth = driver.ExecuteScript("return window.outerWidth;", driverScope);

                Assert.That(initalWidth, Is.LessThan(availWidth));

                driver.MaximiseWindow(driverScope);

                Assert.That(driver.ExecuteScript("return window.outerWidth;", driverScope), Is.GreaterThanOrEqualTo(availWidth));
            }

            public static void AssertResizesWindow(Driver driver, Scope driverScope)
            {
                var availWidth = driver.ExecuteScript("return window.screen.availWidth;", driverScope);
                var initalWidth = driver.ExecuteScript("return window.outerWidth;", driverScope);

                Assert.That(initalWidth, Is.LessThan(availWidth));

                driver.ResizeTo(new Size(768, 500), driverScope);

                Assert.That(768, Is.EqualTo(driver.ExecuteScript("return window.outerWidth;", driverScope)));
                Assert.That(500, Is.EqualTo(driver.ExecuteScript("return window.outerHeight;", driverScope)));
            }
        }

        private Driver _driver;
        private DriverScope _scope;

        [SetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void MaximisesWindow()
        {
            Helpers.AssertMaximisesWindow(_driver, _scope);
        }

        [Test]
        public void MaximisesCorrectWindowScope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            try
            {
                Helpers.AssertMaximisesWindow(_driver, popUp);
            }
            finally
            {
                _driver.ExecuteScript("return self.close();", popUp);
            }
        }

        [Test]
        public void ResizesWindow()
        {
            Helpers.AssertResizesWindow(_driver, _scope);
        }

        [Test]
        public void ResizesCorrectWindowScope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            try
            {
                Helpers.AssertResizesWindow(_driver, popUp);
            }
            finally
            {
                _driver.ExecuteScript("return self.close();", popUp);
            }
        }
    }
}