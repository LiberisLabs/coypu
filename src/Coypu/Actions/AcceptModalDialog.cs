namespace Coypu.Actions
{
    internal class AcceptModalDialog : DriverAction
    {
        private readonly DriverScope _driverScope;

        internal AcceptModalDialog(DriverScope driverScope, IDriver driver, Options options) : base(driver, driverScope, options)
        {
            _driverScope = driverScope;
        }

        public override void Act()
        {
            Driver.AcceptModalDialog(_driverScope);
        }
    }
}