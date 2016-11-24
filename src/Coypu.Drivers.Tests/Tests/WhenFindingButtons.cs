using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingButtons
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_a_particular_button_by_its_text()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "first button").Id, Is.EqualTo("firstButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "second button").Id, Is.EqualTo("secondButtonId"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "firstButtonId").Text, Is.EqualTo("first button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdButtonId").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "secondButtonName").Text, Is.EqualTo("second button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdButtonName").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_value()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "first input button").Id, Is.EqualTo("firstInputButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "second input button").Id, Is.EqualTo("secondInputButtonId"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "firstInputButtonId").Value, Is.EqualTo("first input button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdInputButtonId").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "secondInputButtonId").Value, Is.EqualTo("second input button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdInputButtonName").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_value()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "first submit button").Id, Is.EqualTo("firstSubmitButtonId"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "second submit button").Id, Is.EqualTo("secondSubmitButtonId"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "firstSubmitButtonId").Value, Is.EqualTo("first submit button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdSubmitButtonId").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "secondSubmitButtonName").Value, Is.EqualTo("second submit button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "thirdSubmitButtonName").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_image_buttons()
        {
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "firstImageButtonId").Value, Is.EqualTo("first image button"));
            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "secondImageButtonId").Value, Is.EqualTo("second image button"));
        }

        [Test]
        public void Does_not_find_text_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstTextInputId"));
        }

        [Test]
        public void Does_not_find_email_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstEmailInputId"));
        }

        [Test]
        public void Does_not_find_tel_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstTelInputId"));
        }

        [Test]
        public void Does_not_find_url_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstUrlInputId"));
        }

        [Test]
        public void Does_not_find_hidden_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstHiddenInputId"));
        }

        [Test]
        public void Does_not_find_invisible_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "firstInvisibleInputId"));
        }

        [Test]
        public void Finds_img_elements_with_role_button_by_alt_text()
        {
            Assert.That("roleImageButtonId", Is.EqualTo(DriverHelpers.Button(DriverSpecs.Driver, "I'm an image with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_role_button_by_text()
        {
            Assert.That("roleSpanButtonId", Is.EqualTo(DriverHelpers.Button(DriverSpecs.Driver, "I'm a span with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_button_by_text()
        {
            Assert.That("classButtonSpanButtonId", Is.EqualTo(DriverHelpers.Button(DriverSpecs.Driver, "I'm a span with the class of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_btn_by_text()
        {
            Assert.That("classBtnSpanButtonId", Is.EqualTo(DriverHelpers.Button(DriverSpecs.Driver, "I'm a span with the class of btn").Id));
        }

        [Test]
        public void Finds_an_image_button_with_both_types_of_quote_in_my_value()
        {
            var button = DriverHelpers.Button(DriverSpecs.Driver, "I'm an image button with \"both\" types of quote in my value");
            Assert.That("buttonWithBothQuotesId", Is.EqualTo(button.Id));
        }
    }
}