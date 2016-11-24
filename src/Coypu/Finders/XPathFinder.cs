using System;
using System.Text.RegularExpressions;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class XPathFinder : XPathQueryFinder
    {
        private readonly string _text;
        private readonly Regex _textPattern;

        protected string SelectorType => "xpath";

        public XPathFinder(IDriver driver, string locator, DriverScope scope, Options options)
            : base(driver, locator, scope, options)
        {
        }

        public XPathFinder(IDriver driver, string locator, DriverScope scope, Options options, Regex textPattern)
            : base(driver, locator, scope, options)
        {
            _textPattern = textPattern;
        }

        public XPathFinder(IDriver driver, string locator, DriverScope scope, Options options, string text)
            : base(driver, locator, scope, options)
        {
            _text = text;
        }

        public override bool SupportsSubstringTextMatching => true;

        internal override string QueryDescription
        {
            get
            {
                var queryDesciption = SelectorType + ": " + Locator;
                if (_text != null)
                    queryDesciption += " with text " + _text;
                if (_textPattern != null)
                    queryDesciption += " with text matching /" + (_text ?? _textPattern.ToString()) + "/";

                return queryDesciption;
            }
        }

        protected override Func<string, Options, string> GetQuery(Html html)
        {
            return (locator, options) => string.IsNullOrEmpty(_text) ? Locator : Locator + XPath.Where(html.IsText(_text, options));
        }
    }
}