using System;
using Coypu.Drivers.Selenium;

namespace Coypu
{
    /// <summary>
    /// Global configuration settings
    /// </summary>
    public class SessionConfiguration : Options
    {
        private const string DefaultAppHost = "localhost";
        private const int DefaultPort = 80;

        private string _appHost;

        /// <summary>
        /// New default configuration
        /// </summary>
        public SessionConfiguration()
        {
            AppHost = DefaultAppHost;
            Port = DefaultPort;
            SSL = false;
            Browser = Drivers.Browser.Chrome;
            Driver = typeof(SeleniumWebDriver);
        }

        /// <summary>
        /// <para>Specifies the browser you would like to control</para>
        /// <para>Default: Firefox</para>
        /// </summary>
        public Drivers.Browser Browser { get; set; }

        /// <summary>
        /// <para>Specifies the driver you would like to use to control the browser</para> 
        /// <para>Default: SeleniumWebDriver</para>
        /// </summary>
        public Type Driver { get; set; }


        /// <summary>
        /// <para>The host of the website you are testing, e.g. 'github.com'</para>
        /// <para>Default: localhost</para>
        /// </summary>
        public string AppHost
        {
            get { return _appHost; }
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    var uri = new Uri(value);
                    SSL = uri.Scheme == "https";
                    UserInfo = uri.UserInfo;
                    value = uri.Host;
                }
                _appHost = value?.TrimEnd('/');
            }
        }

        internal string UserInfo { get; set; }

        /// <summary>
        /// <para>The port of the website you are testing</para>
        /// <para>Default: 80</para>
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// <para>Whether to use the HTTPS protocol to connect to website you are testing</para>
        /// <para>Default: false</para>
        /// </summary>
        public bool SSL { get; set; }
    }
}