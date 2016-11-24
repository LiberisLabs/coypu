using System;
using System.IO;
using Coypu.Drivers.Selenium;

namespace Coypu.Drivers.Tests
{
    internal class DriverSpecs
    {
        private static Driver _driver;

        public static void VisitTestPage(string testPage = @"html\InteractionTestsPage.htm")
        {
            Driver.Visit(SomeRandomStaticHelpers.TestHtmlPathLocation(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testPage)), Root);
        }

        public static DriverScope Root => DriverHelpers.WindowScope(Driver);

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

        public static Driver Driver => EnsureDriver();

        public static Driver Instance()
        {
            var driver = EnsureDriver();
            VisitTestPage();
            return driver;
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