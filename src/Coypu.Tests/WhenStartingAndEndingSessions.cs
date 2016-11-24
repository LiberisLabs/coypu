using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests
{
    [TestFixture]
    public class WhenStartingAndEndingSessions
    {
        private SessionConfiguration _sessionConfiguration;

        [SetUp]
        public void SetUp()
        {
            _sessionConfiguration = new SessionConfiguration {Driver = typeof(FakeDriver)};
        }

        [Test]
        public void Dispose_handles_a_disposed_session()
        {
            var browserSession = new BrowserSession(_sessionConfiguration);

            browserSession.Dispose();
            browserSession.Dispose();
        }

        [Test]
        public void A_session_gets_its_driver_from_config()
        {
            _sessionConfiguration.Driver = typeof(FakeDriver);
            using (var browserSession = new BrowserSession(_sessionConfiguration))
            {
                Assert.That(browserSession.Driver, Is.TypeOf(typeof(FakeDriver)));
            }

            _sessionConfiguration.Driver = typeof(StubDriver);
            using (var browserSession = new BrowserSession(_sessionConfiguration))
            {
                Assert.That(browserSession.Driver, Is.TypeOf(typeof(StubDriver)));
            }
        }

        [Test]
        public void Session_exposes_native_driver_if_you_really_need_it()
        {
            using (var browserSession = new BrowserSession(_sessionConfiguration))
            {
                Assert.That(browserSession.Native, Is.EqualTo("Native driver on fake driver"));
            }
        }
    }
}