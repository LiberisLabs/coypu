using System;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class IdFinder : XPathQueryFinder
    {
        internal IdFinder(IDriver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }

        public override bool SupportsSubstringTextMatching => false;

        protected override Func<string, Options, string> GetQuery(Html html) => html.Id;

        internal override string QueryDescription => "id: " + Locator;
    }
}