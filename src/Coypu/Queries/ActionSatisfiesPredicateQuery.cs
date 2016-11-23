using Coypu.Actions;
using Coypu.Timing;

namespace Coypu.Queries
{
    internal class ActionSatisfiesPredicateQuery : Query<bool>
    {
        private readonly BrowserAction tryThis;
        private readonly PredicateQuery until;
        private readonly TimingStrategy timingStrategy;

        public Options Options { get; private set; }
        public DriverScope Scope { get; private set; }

        internal ActionSatisfiesPredicateQuery(BrowserAction tryThis, PredicateQuery until, Options options, TimingStrategy timingStrategy)
        {
            this.tryThis = tryThis;
            this.until = until;
            this.timingStrategy = timingStrategy;
            Options = options;
            Scope = tryThis.Scope;
        }

        public bool Run()
        {
            tryThis.Act();
            return timingStrategy.Synchronise(until);
        }

        public object ExpectedResult
        {
            get { return true; }
        }
    }
}