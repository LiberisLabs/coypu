namespace Coypu.Queries
{
    internal class ElementQuery : DriverScopeQuery<IElement>
    {
        public ElementQuery(DriverScope driverScope, Options options) : base(driverScope, options)
        {
        }

        public override object ExpectedResult => null;

        public override IElement Run()
        {
            return Scope.FindElement();
        }
    }
}