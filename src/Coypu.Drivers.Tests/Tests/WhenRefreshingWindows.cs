using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenRefreshingWindows
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void RefreshCausesPageToReload()
        {
            RefreshCausesScopeToReload(DriverSpecs.Root);
        }

        [Test]
        public void RefreshesCorrectWindowScope()
        {
            DriverSpecs.Driver.Click(DriverHelpers.Link(DriverSpecs.Driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(DriverSpecs.Driver,"Pop Up Window", DriverSpecs.Root, Default.Options), DriverSpecs.Driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            try
            {
                RefreshCausesScopeToReload(popUp);
            }
            finally
            {
                DriverSpecs.Driver.ExecuteScript("return self.close();", popUp);
            }
        }

        private static void RefreshCausesScopeToReload(Scope driverScope)
        {
            var tickBeforeRefresh = (long)DriverSpecs.Driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            DriverSpecs.Driver.Refresh(driverScope);

            var tickAfterRefresh = (long)DriverSpecs.Driver.ExecuteScript("return window.SpecData.CurrentTick;", driverScope);

            Assert.That(tickAfterRefresh, Is.GreaterThan(tickBeforeRefresh));
        }
    }
}