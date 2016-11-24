using Coypu.Actions;
using Coypu.Finders;
using Coypu.Timing;

namespace Coypu
{
    public class SelectFrom
    {
        private readonly string _option;
        private readonly IDriver _driver;
        private readonly ITimingStrategy _timingStrategy;
        private readonly DriverScope _scope;
        private readonly Options _options;
        private readonly IDisambiguationStrategy _disambiguationStrategy;

        internal SelectFrom(string option, IDriver driver, ITimingStrategy timingStrategy, DriverScope scope, Options options, IDisambiguationStrategy disambiguationStrategy)
        {
            _option = option;
            _driver = driver;
            _timingStrategy = timingStrategy;
            _scope = scope;
            _options = options;
            _disambiguationStrategy = disambiguationStrategy;
        }

        /// <summary>
        /// Find the first matching select to appear within the SessionConfiguration.Timeout from which to select this option.
        /// </summary>
        /// <param name="locator">The text of the associated label element, the id or name</param>
        /// <exception cref="T:Coypu.MissingHtmlException">Thrown if the element cannot be found</exception>
        public void From(string locator)
        {
            _timingStrategy.Synchronise(new Select(_driver, _scope, locator, _option, _disambiguationStrategy, _options));
        }
    }
}