using Coypu.Finders;
using Coypu.Tests.WhenInteractingWithTheBrowser;
using Coypu.Timing;
using Coypu.WebRequests;

namespace Coypu.Tests.TestBuilders
{
    internal class TestSessionBuilder
    {
        internal static BrowserSession Build(SessionConfiguration sessionConfiguration,
                                             IDriver driver,
                                             ITimingStrategy timingStrategy,
                                             IWaiter waiter,
                                             IRestrictedResourceDownloader restrictedResourceDownloader,
                                             IUrlBuilder urlBuilder,
                                             IDisambiguationStrategy disambiguationStrategy = null)
        {
            disambiguationStrategy = disambiguationStrategy ?? new FirstOrDefaultNoDisambiguationStrategy();

            return new BrowserSession(sessionConfiguration,
                                      new StubDriverFactory(driver),
                                      timingStrategy,
                                      waiter,
                                      urlBuilder,
                                      disambiguationStrategy,
                                      restrictedResourceDownloader);
        }
    }
}