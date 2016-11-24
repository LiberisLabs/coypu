using System;
using System.IO;

namespace Coypu.Drivers.Tests
{
    internal static class SomeRandomStaticHelpers
    {
        public static string TestHtmlPathLocation(string testPage)
        {
            var file = new FileInfo(Path.Combine(@"..\..\", testPage)).FullName;
            return "file:///" + file.Replace('\\', '/');
        }

        public static string TestSiteUrl(string path)
        {
            return new Uri(Initialize.TestSite.BaseUri, path).AbsoluteUri;
        }
    }
}