namespace Coypu.Actions
{
    internal class Choose : DriverAction
    {
        private readonly ElementScope _elementScope;

        internal Choose(IDriver driver, ElementScope elementScope, Options options)
            : base(driver, elementScope, options)
        {
            _elementScope = elementScope;
        }

        public override void Act()
        {
            Driver.Choose(_elementScope);
        }
    }
}