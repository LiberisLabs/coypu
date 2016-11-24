using System;
using System.Linq;
using Coypu.Queries;
using Coypu.Timing;

namespace Coypu.Finders
{
    internal class StateFinder
    {
        private readonly ITimingStrategy _timingStrategy;

        public StateFinder(ITimingStrategy timingStrategy)
        {
            this._timingStrategy = timingStrategy;
        }

        internal State FindState(State[] states, IScope scope, Options options)
        {
            var query = new LambdaPredicateQuery(() =>
            {
                var was = _timingStrategy.ZeroTimeout;
                _timingStrategy.ZeroTimeout = true;
                try
                {
                    return ((Func<bool>) (() => states.Any(s => s.CheckCondition())))();
                }
                finally
                {
                    _timingStrategy.ZeroTimeout = was;
                }
            }, options);

            var foundState = _timingStrategy.Synchronise(query);

            if (!foundState)
                throw new MissingHtmlException("None of the given states was reached within the configured timeout.");

            return states.First(e => e.ConditionWasMet);
        }
    }
}