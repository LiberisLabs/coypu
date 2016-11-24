using System;
using System.Collections.Generic;
using System.Net;

namespace Coypu.WebRequests
{
    internal class WebClientWithCookies : WebClient, IRestrictedResourceDownloader
    {
        private IEnumerable<Cookie> _requestCookies;
        private readonly WebRequestCookieInjector _webRequestCookieInjector;

        public WebClientWithCookies()
        {
            _webRequestCookieInjector = new WebRequestCookieInjector();
        }

        public void SetCookies(IEnumerable<Cookie> cookies)
        {
            _requestCookies = cookies;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            return _webRequestCookieInjector.InjectCookies(base.GetWebRequest(address), _requestCookies);
        }
    }
}