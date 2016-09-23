using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_sections_as_divs : DriverSpecs
    {
        [Test]
        public void Finds_by_h1_text()
        {
            Assert.That(Section("Div Section One h1").Id, Is.EqualTo("divSectionOne"));
            Assert.That(Section("Div Section Two h1").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h2_text()
        {
            Assert.That(Section("Div Section One h2").Id, Is.EqualTo("divSectionOne"));
            Assert.That(Section("Div Section Two h2").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h3_text()
        {
            Assert.That(Section("Div Section One h3").Id, Is.EqualTo("divSectionOne"));
            Assert.That(Section("Div Section Two h3").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h6_text()
        {
            Assert.That(Section("Div Section One h6").Id, Is.EqualTo("divSectionOne"));
            Assert.That(Section("Div Section Two h6").Id, Is.EqualTo("divSectionTwo"));
        }


        [Test]
        public void Finds_by_h2_text_within_child_link()
        {
            Assert.That(Section("Div Section One h2 with link").Id, Is.EqualTo("divSectionOneWithLink"));
            Assert.That(Section("Div Section Two h2 with link").Id, Is.EqualTo("divSectionTwoWithLink"));
        }


        [Test]
        public void Finds_by_div_by_id()
        {
            Assert.That(Section("divSectionOne").Native, Is.EqualTo(Section("Div Section One h1").Native));
            Assert.That(Section("divSectionTwo").Native, Is.EqualTo(Section("Div Section Two h1").Native));
        }
    }
}