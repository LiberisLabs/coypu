using System.Collections.Generic;
using System.Net;

namespace Coypu.WebRequests
{
    internal class WebRequestCookieInjector
    {
        internal WebRequest InjectCookies(WebRequest webRequest, IEnumerable<Cookie> cookies)
        {
            var request = webRequest;

            return request is HttpWebRequest
                ? AddCookiesToCookieContainer((HttpWebRequest) request, cookies)
                : request;
        }

        internal static HttpWebRequest AddCookiesToCookieContainer(HttpWebRequest httpRequest,
                                                                   IEnumerable<Cookie> cookies)
        {
            httpRequest.CookieContainer = new CookieContainer();

            foreach (var cookie in cookies)
                httpRequest.CookieContainer.Add(cookie);

            return httpRequest;
        }
    }
}