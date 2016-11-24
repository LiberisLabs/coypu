using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenInspectingTitle : BrowserInteractionTests
    {
        [Test]
        public void It_returns_the_driver_page_title()
        {
            const string pageTitle = "Coypu interaction tests page";
            driver.StubTitle(pageTitle, browserSession);
            Assert.That(browserSession.Title, Is.EqualTo(pageTitle));
        }
    }
}