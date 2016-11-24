using System;
using Coypu.Actions;
using Coypu.Finders;
using Coypu.Queries;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class SynchronisedElementScope : ElementScope
    {
        private readonly Options _options;

        internal SynchronisedElementScope(ElementFinder elementFinder, DriverScope outerScope, Options options)
            : base(elementFinder, outerScope)
        {
            _options = options;
        }

        internal override bool Stale { get; set; }

        /// <inheritdoc />
        public override IElement Now()
        {
            return timingStrategy.Synchronise(new ElementQuery(this, _options));
        }

        internal override void Try(DriverAction action)
        {
            RetryUntilTimeout(action);
        }

        /// <summary>
        /// <para>Check if this element exists within the <see cref="Options.Timeout"/></para>
        /// </summary>
        /// <param name="options">
        /// <para>Override the way Coypu is configured to find elements for this call only.</para>
        /// <para>E.g. A longer wait:</para>
        /// 
        /// <code>new Options{Timeout = TimeSpan.FromSeconds(60)}</code></param>
        public override bool Exists(Options options = null)
        {
            return Try(new ElementExistsQuery(this, Merge(options)));
        }

        /// <summary>
        /// <para>Check if this element becomes missing within the <see cref="Options.Timeout"/></para>
        /// </summary>
        /// <param name="options">
        /// <para>Override the way Coypu is configured to find elements for this call only.</para>
        /// <para>E.g. A longer wait:</para>
        /// 
        /// <code>new Options{Timeout = TimeSpan.FromSeconds(60)}</code></param>
        public override bool Missing(Options options = null)
        {
            return Try(new ElementMissingQuery(this, Merge(options)));
        }

        internal override bool Try(IQuery<bool> query)
        {
            return Query(query);
        }

        internal override T Try<T>(Func<T> getAttribute)
        {
            return Query(new LambdaQuery<T>(getAttribute, null, this, _options));
        }
    }
}