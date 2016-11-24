using Coypu.Finders;

namespace Coypu.Actions
{
    internal class Select : DriverAction
    {
        private readonly string _locator;
        private readonly string _optionToSelect;
        private readonly Options _options;
        private readonly IDisambiguationStrategy _disambiguationStrategy;
        private ElementScope _selectElement;

        internal Select(IDriver driver, DriverScope scope, string locator, string optionToSelect, IDisambiguationStrategy disambiguationStrategy, Options options)
            : base(driver, scope, options)
        {
            _locator = locator;
            _optionToSelect = optionToSelect;
            _options = options;
            _disambiguationStrategy = disambiguationStrategy;
        }

        internal Select(IDriver driver, ElementScope selectElement, string optionToSelect, IDisambiguationStrategy disambiguationStrategy, Options options)
            : base(driver, selectElement, options)
        {
            _selectElement = selectElement;
            _optionToSelect = optionToSelect;
            _options = options;
            _disambiguationStrategy = disambiguationStrategy;
        }

        public override void Act()
        {
            _selectElement = _selectElement ?? FindSelectElement();
            SelectOption(_selectElement);
        }

        private SnapshotElementScope FindSelectElement()
        {
            var selectElementFound = _disambiguationStrategy.ResolveQuery(new SelectFinder(Driver, _locator, Scope, _options));
            return new SnapshotElementScope(selectElementFound, Scope, _options);
        }

        private void SelectOption(DriverScope selectElementScope)
        {
            var option = _disambiguationStrategy.ResolveQuery(new OptionFinder(Driver, _optionToSelect, selectElementScope, _options));
            Driver.Click(option);
        }
    }
}