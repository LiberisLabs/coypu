using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenRefreshingWindows
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [OneTimeTearDown]
        public void TearDown() => _driver.Dispose();

        [Test]
        public void RefreshCausesPageToReload()
        {
            RefreshCausesScopeToReload(_scope, _driver);
        }

        [Test]
        public void RefreshesCorrectWindowScope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver,"Pop Up Window", _scope, Default.Options), _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            try
            {
                RefreshCausesScopeToReload(popUp, _driver);
            }
            finally
            {
                _driver.ExecuteScript("return self.close();", popUp);
            }
        }

        private static void RefreshCausesScopeToReload(Scope driverScope, Driver driver)
        {
            var tickBeforeRefresh = (long)driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            driver.Refresh(driverScope);

            var tickAfterRefresh = (long)driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            Assert.That(tickAfterRefresh, Is.GreaterThan(tickBeforeRefresh));
        }
    }
}