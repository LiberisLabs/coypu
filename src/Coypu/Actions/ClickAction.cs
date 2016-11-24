namespace Coypu.Actions
{
    internal class ClickAction : DriverAction
    {
        private readonly ElementScope _elementScope;

        internal ClickAction(ElementScope elementScope, IDriver driver, Options options)
            : base(driver, elementScope, options)
        {
            _elementScope = elementScope;
        }

        public override void Act()
        {
            Driver.Click(_elementScope);
        }
    }
}