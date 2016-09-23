using NUnit.Framework;
namespace Coypu.Drivers.Tests
{
    internal class When_inspecting_dialog_text : DriverSpecs
    {
        [Test]
        public void Finds_exact_text_in_alert()
        {
            using (Driver)
            {
                Driver.Click(Link("Trigger an alert"));
                Assert.That(Driver.HasDialog("You have triggered an alert and this is the text.", Root), Is.True);
                Assert.That(Driver.HasDialog("You have triggered a different alert and this is the different text.", Root), Is.False);
            }
        }
        [Test]
        public void Finds_exact_text_in_confirm()
        {
            using (Driver)
            {
                Driver.Click(Link("Trigger a confirm"));
                Assert.That(Driver.HasDialog("You have triggered a confirm and this is the text.", Root), Is.True);
                Assert.That(Driver.HasDialog("You have triggered a different confirm and this is the different text.", Root), Is.False);
            }
        }
    }
}