using Coypu.Drivers;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class ShowModalDialog
    {
        [Test, Ignore("Didn't work from original fork")]
        public void Modal_dialog()
        {
            using (var session = new BrowserSession(new SessionConfiguration{Browser = Browser.InternetExplorer}))
            {
                VisitTestPage(session);

                var linkId = session.FindLink("Open modal dialog").Id;
                session.ExecuteScript($"window.setTimeout(function() {{document.getElementById('{linkId}').click()}},1);");

                var dialog = session.FindWindow("Pop Up Window");
                dialog.FillIn("text input in popup").With("I'm interacting with a modal dialog");
                dialog.ClickButton("close");
            }
        }

        private static void VisitTestPage(BrowserWindow browserSession)
        {
            browserSession.Visit(Helper.GetProjectFile(@"html\InteractionTestsPage.htm"));
        }
    }
}