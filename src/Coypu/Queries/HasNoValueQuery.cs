namespace Coypu.Queries
{
    internal class HasNoValueQuery : ElementScopeQuery<bool>
    {
        private readonly string _text;

        public override object ExpectedResult => true;

        internal HasNoValueQuery(DriverScope scope, string text, Options options)
            : base(scope, options)
        {
            _text = text;
        }

        public override bool Run()
        {
            return DriverScope.FindElement().Value != _text;
        }
    }
}