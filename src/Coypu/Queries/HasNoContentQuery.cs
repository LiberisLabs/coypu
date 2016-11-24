namespace Coypu.Queries
{
    internal class HasNoContentQuery : DriverScopeQuery<bool>
    {
        private readonly string _text;

        public override object ExpectedResult => true;

        protected internal HasNoContentQuery(DriverScope scope, string text, Options options) : base(scope, options)
        {
            _text = text;
        }

        public override bool Run()
        {
            return !Scope.FindElement().Text.Contains(_text);
        }
    }
}