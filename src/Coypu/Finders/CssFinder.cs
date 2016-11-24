using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Coypu.Finders
{
    internal class CssFinder : WithTextFinder
    {
        protected override string SelectorType => "css";

        public CssFinder(IDriver driver, string locator, DriverScope scope, Options options)
            : base(driver, locator, scope, options)
        {
        }

        public CssFinder(IDriver driver, string locator, DriverScope scope, Options options, Regex textPattern)
            : base(driver, locator, scope, options, textPattern)
        {
        }

        public CssFinder(IDriver driver, string locator, DriverScope scope, Options options, string text)
            : base(driver, locator, scope, options, text)
        {
        }

        public override bool SupportsSubstringTextMatching => true;

        internal override IEnumerable<IElement> Find(Options options)
        {
            return Driver.FindAllCss(Locator, Scope, options, TextPattern(options.TextPrecision == TextPrecision.Exact));
        }

        public Regex TextPattern(bool exact)
        {
            return Text != null ? TextAsRegex(Text, exact) : textPattern;
        }
    }
}