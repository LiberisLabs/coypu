using System;
using System.IO;
using Coypu.Drivers.Selenium;

namespace Coypu.Drivers.Tests
{
    internal class TestDriver
    {
        private static IDriver _driver;

        public static IDriver Instance(string testPage = @"html\InteractionTestsPage.htm")
        {
            var driver = EnsureDriver();
            driver.Visit(SomeRandomStaticHelpers.TestHtmlPathLocation(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testPage)), DriverHelpers.WindowScope(driver));
            return driver;
        }

        private static IDriver EnsureDriver()
        {
            var driverType = typeof(SeleniumWebDriver);

            if (_driver != null && !_driver.Disposed)
            {
                if (driverType == _driver.GetType())
                    return _driver;

                _driver.Dispose();
            }

            _driver = (IDriver) Activator.CreateInstance(driverType, Browser.Chrome);

            return _driver;
        }

        public static void DisposeDriver()
        {
            if (_driver != null && !_driver.Disposed)
            {
                _driver.Dispose();
            }
        }
    }
}