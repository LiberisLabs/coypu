using System;
using System.Diagnostics;
using Coypu.Timing;
using NUnit.Framework;

namespace Coypu.Tests.WhenMakingBrowserInteractionsRobust
{
    [TestFixture]
    public class WhenTryingAnActionUntilAGivenStateIsReached
    {
        private RetryUntilTimeoutTimingStrategy _retryUntilTimeoutTimingStrategy;
        private Options _options;

        [SetUp]
        public void SetUp()
        {
            _options = new Options {Timeout = TimeSpan.FromMilliseconds(200), RetryInterval = TimeSpan.FromMilliseconds(10)};
            _retryUntilTimeoutTimingStrategy = new RetryUntilTimeoutTimingStrategy();
        }

        [Test]
        public void When_state_exists_It_returns_immediately()
        {
            var toTry = new CountTriesAction(_options);
            var retryInterval1 = TimeSpan.FromMilliseconds(10);
            var until = new AlwaysSucceedsPredicateQuery(true, TimeSpan.Zero, retryInterval1);

            _retryUntilTimeoutTimingStrategy.TryUntil(toTry, until, new Options {Timeout = TimeSpan.FromMilliseconds(20)});

            Assert.That(toTry.Tries, Is.EqualTo(1));
        }

        [Test]
        public void When_state_exists_after_three_tries_It_tries_three_times()
        {
            _options.RetryInterval = TimeSpan.FromMilliseconds(1000);
            var toTry = new CountTriesAction(_options);

            var until = new ThrowsThenSubsequentlySucceedsPredicateQuery(true, 2, new Options {Timeout = TimeSpan.FromMilliseconds(1000), RetryInterval = _options.RetryInterval});

            _retryUntilTimeoutTimingStrategy.TryUntil(toTry, until, new Options {Timeout = TimeSpan.FromMilliseconds(200)});

            Assert.That(toTry.Tries, Is.EqualTo(3));
        }

        [Test, Ignore("Didn't work from original fork")]
        public void When_state_never_exists_It_fails_after_timeout()
        {
            var toTry = new CountTriesAction(_options);
            var until = new AlwaysSucceedsPredicateQuery(false, TimeSpan.Zero, _options.RetryInterval);

            var stopwatch = Stopwatch.StartNew();
            var timeout1 = TimeSpan.FromMilliseconds(200);
            Assert.Throws<MissingHtmlException>(() => _retryUntilTimeoutTimingStrategy.TryUntil(toTry, until, new Options {Timeout = timeout1}));

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            Assert.That(elapsedMilliseconds,
                        Is.GreaterThan(timeout1.TotalMilliseconds -
                                       (_options.RetryInterval.Milliseconds + WhenWaiting.AccuracyMilliseconds)));
            Assert.That(elapsedMilliseconds,
                        Is.LessThan(timeout1.TotalMilliseconds + (_options.RetryInterval.Milliseconds + WhenWaiting.AccuracyMilliseconds)));
        }

        [Test]
        public void When_state_never_exists_It_fails_after_timeout_waiting_before_retry()
        {
            var toTry = new CountTriesAction(_options);
            var until = new ThrowsThenSubsequentlySucceedsPredicateQuery(true, 2, new Options {Timeout = TimeSpan.FromMilliseconds(250), RetryInterval = TimeSpan.FromMilliseconds(200)});

            var timeout1 = TimeSpan.FromMilliseconds(200);
            Assert.Throws<TestException>(() => _retryUntilTimeoutTimingStrategy.TryUntil(toTry, until, new Options {Timeout = timeout1}));
        }

        [Test]
        public void It_applies_the_retryAfter_timeout_within_until()
        {
            var toTry = new CountTriesAction(_options);
            var retryAfter = TimeSpan.FromMilliseconds(20);
            var until = new AlwaysThrowsPredicateQuery<TestException>(TimeSpan.Zero, retryAfter);

            Assert.Throws<TestException>(() => _retryUntilTimeoutTimingStrategy.TryUntil(toTry, until, new Options {Timeout = _options.Timeout}));

            Assert.That(toTry.Tries, Is.GreaterThan(1));
            Assert.That(toTry.Tries, Is.LessThan(12));
        }
    }
}