using Coypu.Actions;
using Coypu.Timing;

namespace Coypu.Queries
{
    internal class ActionSatisfiesPredicateQuery : IQuery<bool>
    {
        private readonly BrowserAction _tryThis;
        private readonly PredicateQuery _until;
        private readonly ITimingStrategy _timingStrategy;

        public Options Options { get; }
        public DriverScope Scope { get; }

        internal ActionSatisfiesPredicateQuery(BrowserAction tryThis, PredicateQuery until, Options options, ITimingStrategy timingStrategy)
        {
            _tryThis = tryThis;
            _until = until;
            _timingStrategy = timingStrategy;
            Options = options;
            Scope = tryThis.Scope;
        }

        public bool Run()
        {
            _tryThis.Act();
            return _timingStrategy.Synchronise(_until);
        }

        public object ExpectedResult => true;
    }
}