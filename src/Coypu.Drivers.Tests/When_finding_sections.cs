﻿using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_sections : DriverSpecs
    {
        [Test]
        public void Finds_by_h1_text()
        {
            Assert.That(Section("Section One h1").Id, Is.EqualTo("sectionOne"));
            Assert.That(Section("Section Two h1").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h2_text()
        {
            Assert.That(Section("Section One h2").Id, Is.EqualTo("sectionOne"));
            Assert.That(Section("Section Two h2").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h3_text()
        {
            Assert.That(Section("Section One h3").Id, Is.EqualTo("sectionOne"));
            Assert.That(Section("Section Two h3").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h6_text()
        {
            Assert.That(Section("Section One h6").Id, Is.EqualTo("sectionOne"));
            Assert.That(Section("Section Two h6").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_section_by_id()
        {
            Assert.That(Section("sectionOne").Id, Is.EqualTo("sectionOne"));
            Assert.That(Section("sectionTwo").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Only_finds_div_and_section()
        {
            Assert.Throws<MissingHtmlException>(() => Section("scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => Section("fieldsetScope2"));
        }
    }
}