using System;
using Coypu.Finders;
using Coypu.Timing;

namespace Coypu.Actions
{
    internal class WaitThenClick : DriverAction
    {
        private readonly IWaiter _waiter;
        private readonly ElementFinder _elementFinder;
        private readonly IDisambiguationStrategy _disambiguationStrategy;
        private readonly TimeSpan _waitBeforeClick;

        internal WaitThenClick(IDriver driver, DriverScope scope, Options options, IWaiter waiter, ElementFinder elementFinder, IDisambiguationStrategy disambiguationStrategy)
            : base(driver, scope, options)
        {
            _waitBeforeClick = options.WaitBeforeClick;
            _waiter = waiter;
            _elementFinder = elementFinder;
            _disambiguationStrategy = disambiguationStrategy;
        }

        public override void Act()
        {
            var element = _disambiguationStrategy.ResolveQuery(_elementFinder);
            _waiter.Wait(_waitBeforeClick);
            Driver.Click(element);
        }
    }
}