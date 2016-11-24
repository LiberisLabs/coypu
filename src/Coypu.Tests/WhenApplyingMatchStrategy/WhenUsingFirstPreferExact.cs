using System.Collections.Generic;
using System.Linq;
using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.WhenApplyingMatchStrategy
{
    [TestFixture]
    public class WhenUsingFirstPreferExact : AnyMatchStrategy
    {
        public override Match Match => Match.First;
        public override TextPrecision TextPrecision => TextPrecision.PreferExact;

        [Test]
        public void When_there_are_multiple_exact_matches_it_checks_only_exact_and_returns_the_first_exact_one()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions);
            var exactResults = new List<IElement> {new StubElement(), new StubElement()};

            StubExactResults(finder, finderOptions, exactResults);

            var results = ResolveQuery(finder);

            Assert.That(results, Is.SameAs(exactResults.First()));
        }

        [Test]
        public void When_there_are_no_matches_it_throws_missing_exception()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions, queryDescription: "something from StubElementFinder");

            StubExactResults(finder, finderOptions, new List<IElement>());
            StubSubstringResults(finder, finderOptions, new List<IElement>());

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

        [Test]
        public void When_there_are_no_exact_but_multiple_substring_matches_it_returns_the_first_substring_match()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions);
            var exactResults = new List<IElement>();
            var substringResults = new List<IElement> {new StubElement(), new StubElement()};

            StubExactResults(finder, finderOptions, exactResults);
            StubSubstringResults(finder, finderOptions, substringResults);

            var results = ResolveQuery(finder);

            Assert.That(results, Is.SameAs(substringResults.First()));
        }

        [Test]
        public void When_there_are_no_exact_matches_But_the_finder_does_not_support_substring_text_matching_It_doesnt_bother_trying_substrings()
        {
            var finderOptions = FinderOptions();
            var finder = new StubElementFinder(finderOptions, queryDescription: "something from StubElementFinder",
                                               supportsSubstringTextMatching: false);

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