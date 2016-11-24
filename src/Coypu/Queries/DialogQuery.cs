namespace Coypu.Queries
{
    internal class HasNoDialogQuery : DriverScopeQuery<bool>
    {
        private readonly IDriver _driver;
        private readonly string _text;

        public override object ExpectedResult => true;

        protected internal HasNoDialogQuery(IDriver driver, string text, DriverScope driverScope, Options options) : base(driverScope, options)
        {
            _driver = driver;
            _text = text;
        }

        public override bool Run()
        {
            return !_driver.HasDialog(_text, Scope);
        }
    }
}