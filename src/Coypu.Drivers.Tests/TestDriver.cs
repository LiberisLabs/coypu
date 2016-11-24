using System;
using System.IO;
using Coypu.Drivers.Selenium;

namespace Coypu.Drivers.Tests
{
    internal class TestDriver
    {
        private static Driver _driver;

        public static Driver Instance(string testPage = @"html\InteractionTestsPage.htm")
        {
            var driver = EnsureDriver();
            driver.Visit(SomeRandomStaticHelpers.TestHtmlPathLocation(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testPage)), DriverHelpers.WindowScope(driver));
            return driver;
        }

        private static Driver EnsureDriver()
        {
            var driverType = typeof(SeleniumWebDriver);

            if (_driver != null && !_driver.Disposed)
            {
                if (driverType == _driver.GetType())
                    return _driver;

                _driver.Dispose();
            }

            _driver = (Driver) Activator.CreateInstance(driverType, Browser.Chrome);

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