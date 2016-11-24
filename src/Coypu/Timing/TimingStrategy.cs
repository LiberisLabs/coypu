using System;
using Coypu.Actions;
using Coypu.Queries;

namespace Coypu.Timing
{
    public interface ITimingStrategy
    {
        T Synchronise<T>(IQuery<T> query);
        void TryUntil(BrowserAction tryThis, PredicateQuery until, Options options);
        bool ZeroTimeout { get; set; }
        void SetOverrideTimeout(TimeSpan timeout);
        void ClearOverrideTimeout();
    }
}