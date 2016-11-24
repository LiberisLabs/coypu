using System;
using Coypu.Timing;
using NUnit.Framework;

namespace Coypu.Tests.WhenMakingBrowserInteractionsRobust
{
    [TestFixture]
    public class WhenQueryingForAnExpectedResult
    {
        private RetryUntilTimeoutTimingStrategy _retryUntilTimeoutTimingStrategy;
        private TimeSpan _retryInterval;

        [SetUp]
        public void SetUp()
        {
            _retryInterval = TimeSpan.FromMilliseconds(10);
            _retryUntilTimeoutTimingStrategy = new RetryUntilTimeoutTimingStrategy();
        }

        [Test]
        public void When_the_expected_result_is_found_It_returns_the_expected_result_immediately()
        {
            var expectedResult = new object();

            var actualResult = _retryUntilTimeoutTimingStrategy.Synchronise(new AlwaysSucceedsQuery<object>(expectedResult, expectedResult, TimeSpan.FromMilliseconds(200), _retryInterval));

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_the_expected_result_is_never_found_It_returns_the_actual_result_after_timeout()
        {
            var expectedTimeout = TimeSpan.FromMilliseconds(250);
            const int retryIntervalMs = 70;
            _retryInterval = TimeSpan.FromMilliseconds(retryIntervalMs);

            var expectedResult = new object();
            var unexpectedResult = new object();

            var query = new AlwaysSucceedsQuery<object>(unexpectedResult, expectedResult, expectedTimeout, TimeSpan.FromMilliseconds(retryIntervalMs));

            var actualResult = _retryUntilTimeoutTimingStrategy.Synchronise(query);

            Assert.That(actualResult, Is.EqualTo(unexpectedResult));
            Assert.That((int) query.LastCall, Is.InRange(expectedTimeout.Milliseconds - retryIntervalMs,
                                                         expectedTimeout.Milliseconds + retryIntervalMs));
        }

        [Test]
        public void When_exceptions_are_always_thrown_It_rethrows_eventually()
        {
            Assert.Throws<TestException>(() => _retryUntilTimeoutTimingStrategy.Synchronise(new AlwaysThrowsQuery<object, TestException>(new Options
            {
                Timeout = TimeSpan.FromMilliseconds(200),
                RetryInterval = _retryInterval
            })));
        }

        [Test]
        public void When_exceptions_are_thrown_It_retries_And_when_expected_result_found_subsequently_It_returns_expected_result_immediately()
        {
            const int throwsHowManyTimes = 2;
            var expectedResult = new object();
            var query = new ThrowsThenSubsequentlySucceedsQuery<object>(expectedResult, expectedResult, throwsHowManyTimes,
                                                                        new Options {Timeout = TimeSpan.FromMilliseconds(100), RetryInterval = _retryInterval});

            Assert.That(_retryUntilTimeoutTimingStrategy.Synchronise(query), Is.EqualTo(expectedResult));
            Assert.That(query.Tries, Is.EqualTo(throwsHowManyTimes + 1));
        }

        [Test]
        public void When_a_not_supported_exception_is_thrown_It_does_not_retry()
        {
            var throwsNotSupported = new AlwaysThrowsQuery<object, NotSupportedException>(new Options {Timeout = TimeSpan.FromMilliseconds(200), RetryInterval = _retryInterval});

            Assert.Throws<NotSupportedException>(() => _retryUntilTimeoutTimingStrategy.Synchronise(throwsNotSupported));
            Assert.That(throwsNotSupported.Tries, Is.EqualTo(1));
        }

        [Test]
        public void When_exceptions_are_thrown_It_retries_And_when_unexpected_result_found_subsequently_It_keeps_looking_for_expected_result_But_returns_unexpected_result_after_timeout()
        {
            var expectedTimeout = TimeSpan.FromMilliseconds(250);
            const int retryIntervalMs = 70;
            _retryInterval = TimeSpan.FromMilliseconds(retryIntervalMs);

            var expectedResult = new object();
            var unexpectedResult = new object();

            var throwsTwiceTimesThenReturnOppositeResult = new ThrowsThenSubsequentlySucceedsQuery<object>(unexpectedResult, expectedResult, 2,
                                                                                                           new Options {Timeout = expectedTimeout, RetryInterval = _retryInterval});

            Assert.That(_retryUntilTimeoutTimingStrategy.Synchronise(throwsTwiceTimesThenReturnOppositeResult), Is.EqualTo(unexpectedResult));
            Assert.That(throwsTwiceTimesThenReturnOppositeResult.Tries, Is.GreaterThanOrEqualTo(3));
            Assert.That((int) throwsTwiceTimesThenReturnOppositeResult.LastCall, Is.InRange(expectedTimeout.Milliseconds - retryIntervalMs,
                                                                                            expectedTimeout.Milliseconds + retryIntervalMs));
        }
    }
}