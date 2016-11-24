using System.Collections.Generic;

namespace Coypu.Finders
{
    internal class DocumentElementFinder : ElementFinder
    {
        private IElement _window;

        public DocumentElementFinder(IDriver driver, Options options) : base(driver, "Window", null, options)
        {
        }

        public override bool SupportsSubstringTextMatching => false;

        internal override IEnumerable<IElement> Find(Options options)
        {
            return new[] {_window = (_window ?? Driver.Window)};
        }

        internal override string QueryDescription => "Document Element";
    }
}