﻿using System;
using System.Text.RegularExpressions;
using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenInspectingThePage : WhenInspecting
    {
        [Test]
        public void HasContent_queries_robustly_Positive_example()
        {
            Check_robust_content_query(true, "some content in which to look", browserSession.HasContent, "content in which");
        }

        [Test]
        public void HasContent_queries_robustly_Negative_example()
        {
            Check_robust_content_query(false, "some content in which to look", browserSession.HasContent, "content not there");
        }

        [Test]
        public void HasContentMatch_queries_robustly_Positive_example()
        {
            Check_robust_content_query(true, "some content in which to look", browserSession.HasContentMatch, new Regex("in wh[iI]ch to look$"));
        }

        [Test]
        public void HasContentMatch_queries_robustly_Negative_example()
        {
            Check_robust_content_query(false, "some content in which to look", browserSession.HasContentMatch, new Regex("some r[eE]gex"));
        }

        [Test]
        public void HasNoContent_queries_robustly_Positive_example()
        {
            Check_robust_content_query(true, "some content in which to look", browserSession.HasNoContent, "content not there");
        }

        [Test]
        public void HasNoContent_queries_robustly_Negative_example()
        {
            Check_robust_content_query(false, "some content in which to look", browserSession.HasNoContent, "content in which");
        }

        [Test]
        public void HasNoContentMatch_queries_robustly_Positive_example()
        {
            Check_robust_content_query(true, "some content in which to look", browserSession.HasNoContentMatch, new Regex("some r[eE]gex"));
        }

        [Test]
        public void HasNoContentMatch_queries_robustly_Negative_example()
        {
            Check_robust_content_query(false, "some content in which to look", browserSession.HasNoContentMatch, new Regex("in wh[iI]ch to look$"));
        }


        private void Check_robust_content_query<T>(bool stubResult, string actualContent, Func<T, Options, bool> subject, T toLookFor)
        {
            var window = new StubElement {Text = actualContent};
            driver.StubCurrentWindow(window);

            SpyTimingStrategy.StubQueryResult(true, !stubResult);

            var individualTimeout = TimeSpan.FromMilliseconds(DateTime.UtcNow.Millisecond);
            var options = new SessionConfiguration {Timeout = individualTimeout};

            var actualImmediateResult = subject(toLookFor, options);

            Assert.That(actualImmediateResult, Is.EqualTo(!stubResult), "Result was not found robustly");

            var queryResult = RunQueryAndCheckTiming<bool>(individualTimeout);

            Assert.That(queryResult, Is.EqualTo(stubResult));
        }
    }
}