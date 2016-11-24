namespace Coypu.Queries
{
    internal class HasContentQuery : DriverScopeQuery<bool>
    {
        private readonly string _text;

        public override object ExpectedResult => true;

        internal HasContentQuery(DriverScope scope, string text, Options options) : base(scope, options)
        {
            _text = text;
        }

        public override bool Run()
        {
            return Scope.FindElement().Text.Contains(_text);
        }
    }
}