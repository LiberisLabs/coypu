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
            Assert.That(DriverSpecs.Button("first button").Id, Is.EqualTo("firstButtonId"));
            Assert.That(DriverSpecs.Button("second button").Id, Is.EqualTo("secondButtonId"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_id()
        {
            Assert.That(DriverSpecs.Button("firstButtonId").Text, Is.EqualTo("first button"));
            Assert.That(DriverSpecs.Button("thirdButtonId").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_name()
        {
            Assert.That(DriverSpecs.Button("secondButtonName").Text, Is.EqualTo("second button"));
            Assert.That(DriverSpecs.Button("thirdButtonName").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_value()
        {
            Assert.That(DriverSpecs.Button("first input button").Id, Is.EqualTo("firstInputButtonId"));
            Assert.That(DriverSpecs.Button("second input button").Id, Is.EqualTo("secondInputButtonId"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_id()
        {
            Assert.That(DriverSpecs.Button("firstInputButtonId").Value, Is.EqualTo("first input button"));
            Assert.That(DriverSpecs.Button("thirdInputButtonId").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_name()
        {
            Assert.That(DriverSpecs.Button("secondInputButtonId").Value, Is.EqualTo("second input button"));
            Assert.That(DriverSpecs.Button("thirdInputButtonName").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_value()
        {
            Assert.That(DriverSpecs.Button("first submit button").Id, Is.EqualTo("firstSubmitButtonId"));
            Assert.That(DriverSpecs.Button("second submit button").Id, Is.EqualTo("secondSubmitButtonId"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_id()
        {
            Assert.That(DriverSpecs.Button("firstSubmitButtonId").Value, Is.EqualTo("first submit button"));
            Assert.That(DriverSpecs.Button("thirdSubmitButtonId").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_name()
        {
            Assert.That(DriverSpecs.Button("secondSubmitButtonName").Value, Is.EqualTo("second submit button"));
            Assert.That(DriverSpecs.Button("thirdSubmitButtonName").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_image_buttons()
        {
            Assert.That(DriverSpecs.Button("firstImageButtonId").Value, Is.EqualTo("first image button"));
            Assert.That(DriverSpecs.Button("secondImageButtonId").Value, Is.EqualTo("second image button"));
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
            Assert.That("roleImageButtonId", Is.EqualTo(DriverSpecs.Button("I'm an image with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_role_button_by_text()
        {
            Assert.That("roleSpanButtonId", Is.EqualTo(DriverSpecs.Button("I'm a span with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_button_by_text()
        {
            Assert.That("classButtonSpanButtonId", Is.EqualTo(DriverSpecs.Button("I'm a span with the class of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_btn_by_text()
        {
            Assert.That("classBtnSpanButtonId", Is.EqualTo(DriverSpecs.Button("I'm a span with the class of btn").Id));
        }

        [Test]
        public void Finds_an_image_button_with_both_types_of_quote_in_my_value()
        {
            var button = DriverSpecs.Button("I'm an image button with \"both\" types of quote in my value");
            Assert.That("buttonWithBothQuotesId", Is.EqualTo(button.Id));
        }
    }
}