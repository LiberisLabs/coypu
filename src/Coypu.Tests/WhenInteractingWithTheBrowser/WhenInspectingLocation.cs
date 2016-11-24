using System;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenInspectingLocation : BrowserInteractionTests
    {
        [Test]
        public void It_returns_the_driver_url()
        {
            var driverLocation = new Uri("https://blank.org:8080/actual_location");
            driver.StubLocation(driverLocation, browserSession);
            Assert.That(browserSession.Location, Is.EqualTo(driverLocation));
        }
    }
}