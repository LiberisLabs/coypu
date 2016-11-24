﻿using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenFindingThenUncheckingAnyElement : BrowserInteractionTests
    {
        [Test]
        public void It_makes_robust_call_to_find_then_clicks_element_on_underlying_driver()
        {
            var element = new StubElement();
            driver.StubId("something_to_click", element, browserSession, sessionConfiguration);
            SpyTimingStrategy.AlwaysReturnFromSynchronise(element);

            var toCheck = browserSession.FindCss("something_to_click");

            toCheck.Uncheck();

            Assert.That(driver.UncheckedElements, Has.No.Member(toCheck), "Uncheck was not synchronised");

            RunQueryAndCheckTiming();

            Assert.That(driver.UncheckedElements, Has.Member(toCheck));
        }
    }
}