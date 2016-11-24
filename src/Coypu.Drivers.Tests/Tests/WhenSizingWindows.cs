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

                Assert.That(driver.ExecuteScript("return window.outerWidth;", driverScope), Is.EqualTo(768));
                Assert.That(driver.ExecuteScript("return window.outerHeight;", driverScope), Is.EqualTo(500));
            }
        }

        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void MaximisesWindow()
        {
            Helpers.AssertMaximisesWindow(_driver, DriverSpecs.Root);
        }

        [Test]
        public void MaximisesCorrectWindowScope()
        {
            _driver.Click(DriverSpecs.Link("Open pop up window"));
            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions),
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
            Helpers.AssertResizesWindow(_driver, DriverSpecs.Root);
        }

        [Test]
        public void ResizesCorrectWindowScope()
        {
            _driver.Click(DriverSpecs.Link("Open pop up window"));
            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions),
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