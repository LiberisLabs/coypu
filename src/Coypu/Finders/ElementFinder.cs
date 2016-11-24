using System;
using System.Collections.Generic;

namespace Coypu.Finders
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ElementFinder
    {
        protected internal readonly IDriver Driver;
        protected readonly DriverScope Scope;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        protected ElementFinder(IDriver driver, string locator, DriverScope scope, Options options)
        {
            Driver = driver;
            Locator = locator;
            Scope = scope;
            Options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        public Options Options { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract bool SupportsSubstringTextMatching { get; }

        internal string Locator { get; }

        internal abstract IEnumerable<IElement> Find(Options options);

        internal abstract string QueryDescription { get; }

        protected internal virtual Exception GetMissingException()
        {
            return new MissingHtmlException("Unable to find " + QueryDescription);
        }

        internal ElementScope AsScope()
        {
            return new SynchronisedElementScope(this, Scope, Options);
        }
    }
}