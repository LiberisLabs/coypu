using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenFindingThenFillingInAnyField : BrowserInteractionTests
    {
        [Test]
        public void It_makes_robust_call_to_find_then_clicks_element_on_underlying_driver()
        {
            var element = new StubElement();
            driver.StubId("something_to_fill_in", element, browserSession, sessionConfiguration);
            SpyTimingStrategy.AlwaysReturnFromSynchronise(element);

            var scope = browserSession.FindId("something_to_fill_in");

            scope.FillInWith("some filled in stuff");

            Assert.That(driver.SetFields.Keys, Has.No.Member(scope));

            RunQueryAndCheckTiming();

            Assert.That(driver.SetFields.Keys, Has.Member(scope));
            Assert.That(driver.SetFields[scope].Value, Is.EqualTo("some filled in stuff"));
        }
    }
}