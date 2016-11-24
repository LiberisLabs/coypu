using System.Collections.Generic;
using System.Net;

namespace Coypu.WebRequests
{
    internal interface IRequestCookieInjector
    {
        WebRequest InjectCookies(WebRequest httpRequest, IEnumerable<Cookie> enumerable);
    }
}