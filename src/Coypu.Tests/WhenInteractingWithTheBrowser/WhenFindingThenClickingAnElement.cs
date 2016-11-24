using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenFindingThenClickingAnElement : BrowserInteractionTests
    {
        [Test]
        public void It_finds_then_synchronises_click_element_on_underlying_driver()
        {
            var element = new StubElement();
            driver.StubId("something_to_click", element, browserSession, sessionConfiguration);
            SpyTimingStrategy.AlwaysReturnFromSynchronise(element);

            var scope = browserSession.FindId("something_to_click");
            scope.Click();

            Assert.That(driver.ClickedElements, Has.No.Member(scope), "Click was not synchronised");

            RunQueryAndCheckTiming();

            Assert.That(driver.ClickedElements, Has.Member(scope));
        }
    }
}