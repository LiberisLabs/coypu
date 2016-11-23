using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenBuildingUrls
    {
        [SetUp]
        public void SetUp()
        {
            _sessionConfiguration = new SessionConfiguration();
            _fullyQualifiedUrlBuilder = new FullyQualifiedUrlBuilder();
        }

        private SessionConfiguration _sessionConfiguration;
        private FullyQualifiedUrlBuilder _fullyQualifiedUrlBuilder;

        [Test]
        public void It_defaults_to_localhost()
        {
            _sessionConfiguration.Port = 81;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration),
                        Is.EqualTo("http://localhost:81/visit/me"));
        }

        [Test]
        public void It_defaults_to_port_80()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st/visit/me"));
        }

        [Test]
        public void It_forms_url_from_host_port_and_virtual_path()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 81;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st:81/visit/me"));
        }

        [Test]
        public void It_handles_missing_leading_slashes_in_virtual_path()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st/visit/me"));
        }

        [Test]
        public void It_handles_basic_auth_provided_in_the_host()
        {
            _sessionConfiguration.AppHost = "http://someone:example@im.theho.st";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("http://someone:example@im.theho.st/visit/me"));
        }

        [Test]
        public void It_handles_protocol_provided_in_the_host()
        {
            _sessionConfiguration.AppHost = "http://im.theho.st";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st/visit/me"));
        }

        [Test]
        public void It_handles_ssl_protocol_provided_in_the_host()
        {
            _sessionConfiguration.AppHost = "https://im.theho.st";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("https://im.theho.st/visit/me"));
        }

        [Test]
        public void SSL_overrides_protocol_provided_in_the_host()
        {
            _sessionConfiguration.AppHost = "http://im.theho.st";
            _sessionConfiguration.SSL = true;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("https://im.theho.st/visit/me"));
        }

        [Test]
        public void It_handles_trailing_and_missing_leading_slashes_with_a_port()
        {
            _sessionConfiguration.AppHost = "im.theho.st/";
            _sessionConfiguration.Port = 123;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st:123/visit/me"));
        }

        [Test]
        public void It_handles_trailing_slashes_in_host()
        {
            _sessionConfiguration.AppHost = "im.theho.st/";
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration),
                        Is.EqualTo("http://im.theho.st/visit/me"));
        }

        [Test]
        public void It_ignores_host_etc_when_supplied_a_fully_qualified_url()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 321;
            _sessionConfiguration.SSL = true;

            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("http://www.someother.site/over.here", _sessionConfiguration),
                        Is.EqualTo("http://www.someother.site/over.here"));
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("file:///C:/local/file.here", _sessionConfiguration),
                        Is.EqualTo("file:///C:/local/file.here"));
        }

        [Test]
        public void It_encodes_the_fully_qualified_url_when_supplied_an_unencoded_relative_url()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 321;
            _sessionConfiguration.SSL = true;

            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/over.here?c=p|pe", _sessionConfiguration),
                        Is.EqualTo("https://im.theho.st:321/over.here?c=p%7Cpe"));
        }

        [Test]
        public void It_encodes_the_fully_qualified_url_when_supplied_an_unencoded_fully_qualified_url()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 321;
            _sessionConfiguration.SSL = true;

            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("http://www.someother.site/over.here?c=p|pe", _sessionConfiguration),
                        Is.EqualTo("http://www.someother.site/over.here?c=p%7Cpe"));
        }

        [Test]
        public void It_supports_SSL()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.SSL = true;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration),
                        Is.EqualTo("https://im.theho.st/visit/me"));
        }

        [Test]
        public void It_supports_SSL_with_ports()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 321;
            _sessionConfiguration.SSL = true;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("/visit/me", _sessionConfiguration), Is.EqualTo("https://im.theho.st:321/visit/me"));
        }

        [Test]
        public void It_ignores_host_when_supplied_a_fully_qualified_url()
        {
            _sessionConfiguration.AppHost = "im.theho.st";
            _sessionConfiguration.Port = 321;
            _sessionConfiguration.SSL = true;
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("http://www.someother.site/over.here", _sessionConfiguration), Is.EqualTo("http://www.someother.site/over.here"));
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("file:///C:/local/file.here", _sessionConfiguration), Is.EqualTo("file:///C:/local/file.here"));
        }

        [Test]
        public void It_ignores_port_when_supplied_a_fully_qualified_url()
        {
            _sessionConfiguration.Port = 321;

            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("http://www.someother.site/over.here", _sessionConfiguration), Is.EqualTo("http://www.someother.site/over.here"));
            Assert.That(_fullyQualifiedUrlBuilder.GetFullyQualifiedUrl("file:///C:/local/file.here", _sessionConfiguration), Is.EqualTo("file:///C:/local/file.here"));
        }
    }
}