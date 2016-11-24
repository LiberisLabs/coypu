using Coypu.Drivers.Selenium;

namespace Coypu.Drivers.Tests
{
    internal static class Default
    {
        public static readonly SessionConfiguration SessionConfiguration = new SessionConfiguration
        {
            Browser = Browser.Chrome,
            Driver = typeof(SeleniumWebDriver),
            TextPrecision = TextPrecision.Exact
        };

        public static readonly Options Options = new Options();
    }
}