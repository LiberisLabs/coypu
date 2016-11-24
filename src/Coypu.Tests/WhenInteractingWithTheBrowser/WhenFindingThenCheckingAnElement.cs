using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenFindingThenCheckingAnElement : BrowserInteractionTests
    {
        [Test]
        public void It_finds_then_synchronises_check_element_on_underlying_driver()
        {
            var element = new StubElement();
            driver.StubId("something_to_click", element, browserSession, sessionConfiguration);
            SpyTimingStrategy.AlwaysReturnFromSynchronise(element);

            var toCheck = browserSession.FindCss("something_to_click");

            toCheck.Check();

            Assert.That(driver.CheckedElements, Has.No.Member(toCheck), "Check was not synchronised");

            RunQueryAndCheckTiming();

            Assert.That(driver.CheckedElements, Has.Member(toCheck));
        }
    }
}