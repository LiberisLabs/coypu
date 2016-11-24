namespace Coypu.Actions
{
    internal class FillIn : DriverAction
    {
        private readonly string _value;
        private readonly ElementScope _element;

        internal FillIn(IDriver driver, ElementScope element, string value, Options options) : base(driver, element, options)
        {
            _element = element;
            _value = value;
        }

        private void BringIntoFocus()
        {
            Driver.Click(_element);
        }

        internal void Set()
        {
            Driver.Set(_element, _value);
        }

        internal void Focus()
        {
            if (_element["type"] != "file")
                BringIntoFocus();
        }

        public override void Act()
        {
            Focus();
            Set();
        }
    }
}