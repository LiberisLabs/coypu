namespace Coypu.Actions
{
    internal class CancelModalDialog : DriverAction
    {
        private readonly DriverScope _driverScope;

        internal CancelModalDialog(DriverScope driverScope, IDriver driver, Options options) : base(driver, driverScope, options)
        {
            _driverScope = driverScope;
        }

        public override void Act()
        {
            Driver.CancelModalDialog(_driverScope);
        }
    }
}