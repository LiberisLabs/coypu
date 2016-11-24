namespace Coypu.Actions
{
    internal class SendKeys : DriverAction
    {
        private readonly string _keys;
        private readonly DriverScope _driverScope;

        internal SendKeys(string keys, DriverScope driverScope, IDriver driver, Options options)
            : base(driver, driverScope, options)
        {
            _keys = keys;
            _driverScope = driverScope;
        }

        public override void Act()
        {
            var element = _driverScope.Now();
            Driver.SendKeys(element, _keys);
        }
    }
}