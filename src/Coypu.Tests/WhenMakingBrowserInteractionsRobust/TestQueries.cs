using System;
using System.Diagnostics;
using Coypu.Queries;

namespace Coypu.Tests.WhenMakingBrowserInteractionsRobust
{
    public class AlwaysSucceedsPredicateQuery : PredicateQuery
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly bool _actualResult;

        public int Tries { get; set; }
        public long LastCall { get; set; }

        public AlwaysSucceedsPredicateQuery(bool actualResult, TimeSpan timeout, TimeSpan retryInterval)
            : base(new Options {Timeout = timeout, RetryInterval = retryInterval})
        {
            _actualResult = actualResult;
            _stopWatch.Start();
        }

        public override bool Predicate()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;

            return _actualResult;
        }
    }

    public class AlwaysSucceedsQuery<T> : IQuery<T>
    {
        public Options Options { get; set; }
        public DriverScope Scope { get; private set; }
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly T _actualResult;
        private readonly T _expecting;

        public int Tries { get; set; }
        public long LastCall { get; set; }

        public AlwaysSucceedsQuery(T actualResult, TimeSpan timeout, TimeSpan retryInterval)
        {
            Options = new Options {Timeout = timeout, RetryInterval = retryInterval};
            _actualResult = actualResult;
            _stopWatch.Start();
        }

        public AlwaysSucceedsQuery(T actualResult, T expecting, TimeSpan timeout, TimeSpan retryInterval)
            : this(actualResult, timeout, retryInterval)
        {
            _expecting = expecting;
        }

        public T Run()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;

            return _actualResult;
        }

        public object ExpectedResult => _expecting;
    }

    public class ThrowsSecondTimeQuery<T> : IQuery<T>
    {
        public Options Options { get; set; }
        public DriverScope Scope { get; private set; }
        private readonly T _result;
        public TimeSpan Timeout { get; set; }

        public ThrowsSecondTimeQuery(T result, Options options)
        {
            Options = options;
            _result = result;
        }

        public T Run()
        {
            Tries++;
            if (Tries == 1)
                throw new TestException("Fails first time");

            return _result;
        }

        public object ExpectedResult => default(T);

        public int Tries { get; set; }
    }

    public class AlwaysThrowsQuery<TResult, TException> : IQuery<TResult> where TException : Exception
    {
        public Options Options { get; set; }
        public DriverScope Scope { get; private set; }
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public AlwaysThrowsQuery(Options options)
        {
            Options = options;
            _stopWatch.Start();
        }

        public TResult Run()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;
            throw (TException) Activator.CreateInstance(typeof(TException), "Test Exception");
        }

        public object ExpectedResult => default(TResult);

        public int Tries { get; set; }
        public long LastCall { get; set; }

        public TimeSpan Timeout { get; set; }
    }

    public class AlwaysThrowsPredicateQuery<TException> : PredicateQuery where TException : Exception
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public AlwaysThrowsPredicateQuery(TimeSpan timeout, TimeSpan retryInterval) : base(new Options {Timeout = timeout, RetryInterval = retryInterval})
        {
            _stopWatch.Start();
        }

        public override bool Predicate()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;
            throw (TException) Activator.CreateInstance(typeof(TException), "Test Exception");
        }

        public int Tries { get; set; }
        public long LastCall { get; set; }
    }

    public class ThrowsThenSubsequentlySucceedsQuery<T> : IQuery<T>
    {
        public Options Options { get; set; }
        public DriverScope Scope { get; private set; }
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly T _actualResult;
        private readonly T _expectedResult;
        private readonly int _throwsHowManyTimes;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _retryInterval;

        public ThrowsThenSubsequentlySucceedsQuery(T actualResult, T expectedResult, int throwsHowManyTimes, Options options)
        {
            Options = options;
            _stopWatch.Start();
            _actualResult = actualResult;
            _expectedResult = expectedResult;
            _throwsHowManyTimes = throwsHowManyTimes;
        }

        public T Run()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;

            if (Tries <= _throwsHowManyTimes)
                throw new TestException("Fails first time");

            return _actualResult;
        }

        public object ExpectedResult => _expectedResult;

        public int Tries { get; set; }
        public long LastCall { get; set; }
    }

    public class ThrowsThenSubsequentlySucceedsPredicateQuery : PredicateQuery
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly bool _actualResult;
        private readonly int _throwsHowManyTimes;

        public ThrowsThenSubsequentlySucceedsPredicateQuery(bool actualResult, int throwsHowManyTimes, Options options)
            : base(options)
        {
            _stopWatch.Start();
            _actualResult = actualResult;
            _throwsHowManyTimes = throwsHowManyTimes;
        }

        public override bool Predicate()
        {
            Tries++;
            LastCall = _stopWatch.ElapsedMilliseconds;

            if (Tries <= _throwsHowManyTimes)
            {
                Console.WriteLine("Fails on try " + Tries + " after " + LastCall + "ms");
                throw new TestException("Fails on try " + Tries + " after " + LastCall + "ms");
            }

            return _actualResult;
        }

        public int Tries { get; set; }
        public long LastCall { get; set; }
    }
}