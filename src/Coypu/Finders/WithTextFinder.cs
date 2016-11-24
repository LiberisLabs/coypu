using System.Text.RegularExpressions;

namespace Coypu.Finders
{
    internal abstract class WithTextFinder : ElementFinder
    {
        protected readonly Regex textPattern;
        protected readonly string Text;

        internal WithTextFinder(IDriver driver, string locator, DriverScope scope, Options options)
            : base(driver, locator, scope, options)
        {
        }

        internal WithTextFinder(IDriver driver, string locator, DriverScope scope, Options options, Regex textPattern)
            : this(driver, locator, scope, options)
        {
            this.textPattern = textPattern;
        }

        internal WithTextFinder(IDriver driver, string locator, DriverScope scope, Options options, string text)
            : this(driver, locator, scope, options)
        {
            Text = text;
        }

        internal override string QueryDescription
        {
            get
            {
                var queryDesciption = SelectorType + ": " + Locator;
                if (textPattern != null)
                    queryDesciption += " with text matching /" + (Text ?? textPattern.ToString()) + "/";

                return queryDesciption;
            }
        }

        protected abstract string SelectorType { get; }

        public static Regex TextAsRegex(string textEquals, bool exact)
        {
            Regex textMatches = null;
            if (textEquals != null)
            {
                var escapedText = Regex.Escape(textEquals);
                if (exact)
                    escapedText = string.Format("^{0}$", escapedText);

                textMatches = new Regex(escapedText, RegexOptions.Multiline);
            }

            return textMatches;
        }
    }
}