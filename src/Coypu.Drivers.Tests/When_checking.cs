using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_checking : DriverSpecs
    {
        [Test]
        public void Checks_an_unchecked_checkbox()
        {
            var checkbox = Field("uncheckedBox"); 
            Assert.That(checkbox.Selected, Is.False);

            Driver.Check(checkbox);

            var findAgain = Field("uncheckedBox");
            Assert.That(findAgain.Selected, Is.True);
        }


        [Test]
        public void Leaves_a_checked_checkbox_checked()
        {
            var checkbox = Field("checkedBox");
            Assert.That(checkbox.Selected, Is.True);

            Driver.Check(checkbox);

            var findAgain = Field("checkedBox");
            Assert.That(findAgain.Selected, Is.True);
        }


        [Test]
        public void Unchecks_a_checked_checkbox()
        {
            var checkbox = Field("checkedBox");
            Assert.That(checkbox.Selected, Is.True);

            Driver.Uncheck(checkbox);

            var findAgain = Field("checkedBox");
            Assert.That(findAgain.Selected, Is.False);
        }


        [Test]
        public void Leaves_an_unchecked_checkbox_unchecked()
        {
            var checkbox = Field("uncheckedBox");
            Assert.That(checkbox.Selected, Is.False);

            Driver.Uncheck(checkbox);

            var findAgain = Field("uncheckedBox");
            Assert.That(findAgain.Selected, Is.False);
        }


        [Test]
        public void Fires_onclick_event_on_check()
        {
            var checkbox = Field("uncheckedBox");
            Assert.That(checkbox.Value, Is.EqualTo("unchecked"));

            Driver.Check(checkbox);

            Assert.That(Field("uncheckedBox", Root).Value, Is.EqualTo("unchecked - clicked"));
        }

        [Test]
        public void Fires_onclick_event_on_uncheck()
        {
            var checkbox = Field("checkedBox");
            Assert.That(checkbox.Value, Is.EqualTo("checked"));

            Driver.Uncheck(checkbox);

            Assert.That(Field("checkedBox", Root).Value, Is.EqualTo("checked - clicked"));
        }
    }
}