using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenRefreshingWindows : DriverSpecs
    {
        [Test]
        public void RefreshCausesPageToReload()
        {
            RefreshCausesScopeToReload(Root);
        }

        [Test]
        public void RefreshesCorrectWindowScope()
        {
            Driver.Click(Link("Open pop up window"));
            var popUp = new BrowserWindow(DefaultSessionConfiguration, new WindowFinder(Driver,"Pop Up Window",Root,DefaultOptions), Driver, null, null, null, DisambiguationStrategy);

            try
            {
                RefreshCausesScopeToReload(popUp);
            }
            finally
            {
                Driver.ExecuteScript("return self.close();", popUp);
            }
        }

        private static void RefreshCausesScopeToReload(Scope driverScope)
        {
            var tickBeforeRefresh = (long) Driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            Driver.Refresh(driverScope);

            var tickAfterRefresh = (long) Driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            Assert.That(tickAfterRefresh, Is.GreaterThan(tickBeforeRefresh));
        }
    }
}