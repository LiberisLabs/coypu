using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    [TestFixture]
    internal class WhenExecutingScript : DriverSpecs
    {
        [Test]
        public void Runs_the_script_in_the_browser()
        {
            Button("firstButtonId").Text.should_be("first button");

            Driver.ExecuteScript("document.getElementById('firstButtonId').innerHTML = 'script executed';", Root);

            Button("firstButtonId").Text.should_be("script executed");
        }

        [Test]
        public void Passes_the_arguments_to_the_browser()
        {
            Button("firstButtonId").Text.should_be("first button");

            Driver.ExecuteScript("arguments[0].innerHTML = 'script executed ' + arguments[1];", Root, Button("firstButtonId"), 5);

            Button("firstButtonId").Text.should_be("script executed 5");
        }
      
        [Test]
        public void Returns_the_result()
        {
            Driver.ExecuteScript("return document.getElementById('firstButtonId').innerHTML;", Root).should_be("first button");
        }
    }
}