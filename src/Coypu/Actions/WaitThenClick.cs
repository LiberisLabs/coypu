﻿using System;
using Coypu.Finders;
using Coypu.Timing;

namespace Coypu.Actions
{
    internal class WaitThenClick : DriverAction
    {
        private readonly Waiter waiter;
        private readonly ElementFinder elementFinder;
        private readonly IDisambiguationStrategy disambiguationStrategy;
        private readonly TimeSpan waitBeforeClick;

        internal WaitThenClick(Driver driver, DriverScope scope, Options options, Waiter waiter, ElementFinder elementFinder, IDisambiguationStrategy disambiguationStrategy)
            : base(driver, scope, options)
        {
            waitBeforeClick = options.WaitBeforeClick;
            this.waiter = waiter;
            this.elementFinder = elementFinder;
            this.disambiguationStrategy = disambiguationStrategy;
        }

        public override void Act()
        {
            var element = disambiguationStrategy.ResolveQuery(elementFinder);
            waiter.Wait(waitBeforeClick);
            Driver.Click(element);
        }
    }
}