using System.Text.RegularExpressions;

namespace Coypu.Queries
{
    internal class HasContentMatchQuery : DriverScopeQuery<bool>
    {
        private readonly Regex _text;

        public override object ExpectedResult => true;

        protected internal HasContentMatchQuery(DriverScope scope, Regex text, Options options) : base(scope, options)
        {
            _text = text;
        }

        public override bool Run()
        {
            return _text.IsMatch(Scope.FindElement().Text);
        }
    }
}