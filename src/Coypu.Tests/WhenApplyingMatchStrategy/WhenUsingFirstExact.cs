﻿using System.Collections.Generic;
using System.Linq;
using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenApplyingMatchStrategy
{
    [TestFixture]
    public class WhenUsingFirstExact : AnyMatchStrategy
    {
        public override Match Match => Match.First;
        public override TextPrecision TextPrecision => TextPrecision.Exact;

        [Test]
        public void When_there_is_more_than_one_match_it_picks_the_first_one()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions);
            var exactResults = new List<IElement> {new StubElement(), new StubElement()};

            StubExactResults(finder, finderOptions, exactResults);

            var results = ResolveQuery(finder);

            Assert.That(results, Is.SameAs(exactResults.First()));
        }

        [Test]
        public void When_there_are_no_exact_matches_it_throws_missing_exception_and_only_looks_for_exact()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions, queryDescription: "something from StubElementFinder");

            StubExactResults(finder, finderOptions, new List<IElement>());

            try
            {
                ResolveQuery(finder);
                Assert.Fail("Expected missing html exception");
            }
            catch (MissingHtmlException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Unable to find something from StubElementFinder"));
            }
        }
    }
}