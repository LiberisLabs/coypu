namespace Coypu.Actions
{
    internal class Uncheck : DriverAction
    {
        private readonly ElementScope _element;

        internal Uncheck(IDriver driver, ElementScope element, Options options)
            : base(driver, element, options)
        {
            _element = element;
        }

        public override void Act()
        {
            Driver.Uncheck(_element);
        }
    }
}