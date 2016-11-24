using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingButtons
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_a_particular_button_by_its_text()
        {
            Assert.That("firstButtonId", Is.EqualTo(DriverSpecs.Button("first button").Id));
            Assert.That("secondButtonId", Is.EqualTo(DriverSpecs.Button("second button").Id));
        }

        [Test]
        public void Finds_a_particular_button_by_its_id()
        {
            Assert.That("first button", Is.EqualTo(DriverSpecs.Button("firstButtonId").Text));
            Assert.That("third button", Is.EqualTo(DriverSpecs.Button("thirdButtonId").Text));
        }

        [Test]
        public void Finds_a_particular_button_by_its_name()
        {
            Assert.That("second button", Is.EqualTo(DriverSpecs.Button("secondButtonName").Text));
            Assert.That("third button", Is.EqualTo(DriverSpecs.Button("thirdButtonName").Text));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_value()
        {
            Assert.That("firstInputButtonId", Is.EqualTo(DriverSpecs.Button("first input button").Id));
            Assert.That("secondInputButtonId", Is.EqualTo(DriverSpecs.Button("second input button").Id));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_id()
        {
            Assert.That("first input button", Is.EqualTo(DriverSpecs.Button("firstInputButtonId").Value));
            Assert.That("third input button", Is.EqualTo(DriverSpecs.Button("thirdInputButtonId").Value));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_name()
        {
            Assert.That("second input button", Is.EqualTo(DriverSpecs.Button("secondInputButtonId").Value));
            Assert.That("third input button", Is.EqualTo(DriverSpecs.Button("thirdInputButtonName").Value));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_value()
        {
            Assert.That("firstSubmitButtonId", Is.EqualTo(DriverSpecs.Button("first submit button").Id));
            Assert.That("secondSubmitButtonId", Is.EqualTo(DriverSpecs.Button("second submit button").Id));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_id()
        {
            Assert.That("first submit button", Is.EqualTo(DriverSpecs.Button("firstSubmitButtonId").Value));
            Assert.That("third submit button", Is.EqualTo(DriverSpecs.Button("thirdSubmitButtonId").Value));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_name()
        {
            Assert.That("second submit button", Is.EqualTo(DriverSpecs.Button("secondSubmitButtonName").Value));
            Assert.That("third submit button", Is.EqualTo(DriverSpecs.Button("thirdSubmitButtonName").Value));
        }

        [Test]
        public void Finds_image_buttons()
        {
            Assert.That("first image button", Is.EqualTo(DriverSpecs.Button("firstImageButtonId").Value));
            Assert.That("second image button", Is.EqualTo(DriverSpecs.Button("secondImageButtonId").Value));
        }

        [Test]
        public void Does_not_find_text_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstTextInputId"));
        }

        [Test]
        public void Does_not_find_email_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstEmailInputId"));
        }

        [Test]
        public void Does_not_find_tel_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstTelInputId"));
        }

        [Test]
        public void Does_not_find_url_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstUrlInputId"));
        }

        [Test]
        public void Does_not_find_hidden_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstHiddenInputId"));
        }

        [Test]
        public void Does_not_find_invisible_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("firstInvisibleInputId"));
        }

        [Test]
        public void Finds_img_elements_with_role_button_by_alt_text()
        {
            Assert.That(DriverSpecs.Button("I'm an image with the role of button").Id, Is.EqualTo("roleImageButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_role_button_by_text()
        {
            Assert.That(DriverSpecs.Button("I'm a span with the role of button").Id, Is.EqualTo("roleSpanButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_class_button_by_text()
        {
            Assert.That(DriverSpecs.Button("I'm a span with the class of button").Id, Is.EqualTo("classButtonSpanButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_class_btn_by_text()
        {
            Assert.That(DriverSpecs.Button("I'm a span with the class of btn").Id, Is.EqualTo("classBtnSpanButtonId"));
        }

        [Test]
        public void Finds_an_image_button_with_both_types_of_quote_in_my_value()
        {
            var button = DriverSpecs.Button("I'm an image button with \"both\" types of quote in my value");
            Assert.That(button.Id, Is.EqualTo("buttonWithBothQuotesId"));
        }
    }
}