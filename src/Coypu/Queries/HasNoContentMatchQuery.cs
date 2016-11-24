using System.Text.RegularExpressions;

namespace Coypu.Queries
{
    internal class HasNoContentMatchQuery : DriverScopeQuery<bool>
    {
        private readonly Regex _text;

        public override object ExpectedResult => true;

        protected internal HasNoContentMatchQuery(DriverScope scope, Regex text, Options options)
            : base(scope, options)
        {
            _text = text;
        }

        public override bool Run()
        {
            return !_text.IsMatch(Scope.FindElement().Text);
        }
    }
}