﻿using System;
using System.Linq;
using Coypu.Tests.TestDoubles;
using Coypu.Tests.WhenInteractingWithTheBrowser;
using NUnit.Framework;

namespace Coypu.Tests.WhenMakingBrowserInteractionsRobust
{
    [TestFixture]
    public class WhenIWantToManageTimingManually : BrowserInteractionTests
    {
        [Test]
        public void RetryUntilTimeout_is_exposed_on_the_session()
        {
            var calledOnWrapper = false;
            browserSession.RetryUntilTimeout(() => { calledOnWrapper = true; });
            SpyTimingStrategy.QueriesRan<object>().First().Run();
            Assert.That(calledOnWrapper, Is.True);
        }

        [Test]
        public void Return_from_RetryUntilTimeout_is_exposed_on_the_session()
        {
            Func<string> function = () => "The expected result";

            SpyTimingStrategy.StubQueryResult(SpyTimingStrategy.NO_EXPECTED_RESULT, "immediate result");

            Assert.That(browserSession.RetryUntilTimeout(function), Is.EqualTo("immediate result"));

            var query = SpyTimingStrategy.QueriesRan<string>().First();
            var queryResult = query.Run();

            Assert.That(queryResult, Is.EqualTo("The expected result"));
        }

        [Test]
        public void TryUntil_is_exposed_on_the_session()
        {
            var tried = false;
            var triedUntil = false;
            Action tryThis = () => tried = true;
            Func<bool> until = () => triedUntil = true;
            var overallTimeout = TimeSpan.FromMilliseconds(1234);

            var options = new Options {Timeout = overallTimeout};
            browserSession.TryUntil(tryThis, until, TimeSpan.Zero, options);

            var tryUntil = SpyTimingStrategy.DeferredTryUntils[0];

            Assert.That(tried, Is.False);
            tryUntil.TryThisBrowserAction.Act();
            Assert.That(tried, Is.True);

            Assert.That(triedUntil, Is.False);
            tryUntil.Until.Run();
            Assert.That(triedUntil, Is.True);

            Assert.That(tryUntil.OverallTimeout, Is.EqualTo(overallTimeout));
        }

        [Test]
        public void Query_is_exposed_on_the_session()
        {
            Func<string> query = () => "query result";

            SpyTimingStrategy.StubQueryResult("expected query result", "immediate query result");

            Assert.That(browserSession.Query(query, "expected query result"), Is.EqualTo("immediate query result"));

            var robustQuery = SpyTimingStrategy.QueriesRan<string>().First();
            var queryResult = robustQuery.Run();

            Assert.That(queryResult, Is.EqualTo("query result"));
        }
    }
}