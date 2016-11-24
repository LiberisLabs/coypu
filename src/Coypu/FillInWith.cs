using Coypu.Actions;
using Coypu.Timing;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class FillInWith
    {
        private readonly IDriver _driver;
        private readonly ITimingStrategy _timingStrategy;
        private readonly Options _options;
        private readonly ElementScope _element;

        internal FillInWith(ElementScope element, IDriver driver, ITimingStrategy timingStrategy, Options options)
        {
            _element = element;
            _driver = driver;
            _timingStrategy = timingStrategy;
            _options = options;
        }

        /// <summary>
        /// Supply a value for the text field
        /// </summary>
        /// <param name="value">The value to fill in</param>
        /// <exception cref="T:Coypu.MissingHtmlException">Thrown if the element cannot be found</exception>
        public void With(string value)
        {
            _timingStrategy.Synchronise(new FillIn(_driver, _element, value, _options));
        }
    }
}