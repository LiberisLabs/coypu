using System;
using Coypu.Drivers;

namespace Coypu.Finders
{
    internal class LinkFinder : XPathQueryFinder
    {
        internal LinkFinder(IDriver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }


        public override bool SupportsSubstringTextMatching => true;

        protected override Func<string, Options, string> GetQuery(Html html) => html.Link;

        internal override string QueryDescription => "link: " + Locator;
    }
}