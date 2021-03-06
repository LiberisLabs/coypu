using System;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class FieldFinder : XPathQueryFinder
    {
        internal FieldFinder(IDriver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }

        public override bool SupportsSubstringTextMatching => true;

        protected override Func<string, Options, string> GetQuery(Html html)
        {
            return html.Field;
        }

        internal override string QueryDescription => "field: " + Locator;
    }
}