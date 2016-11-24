namespace Coypu.Actions
{
    internal class CheckAction : DriverAction
    {
        private readonly ElementScope _element;

        internal CheckAction(IDriver driver, ElementScope element, Options options)
            : base(driver, element, options)
        {
            _element = element;
        }

        public override void Act()
        {
            Driver.Check(_element);
        }
    }
}