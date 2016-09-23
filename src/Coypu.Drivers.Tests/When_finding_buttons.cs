using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_finding_buttons : DriverSpecs
    {
        [Test]
        public void Finds_a_particular_button_by_its_text()
        {
            Assert.That(Button("first button").Id, Is.EqualTo("firstButtonId"));
            Assert.That(Button("second button").Id, Is.EqualTo("secondButtonId"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_id()
        {
            Assert.That(Button("firstButtonId").Text, Is.EqualTo("first button"));
            Assert.That(Button("thirdButtonId").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_name()
        {
            Assert.That(Button("secondButtonName").Text, Is.EqualTo("second button"));
            Assert.That(Button("thirdButtonName").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_value()
        {
            Assert.That(Button("first input button").Id, Is.EqualTo("firstInputButtonId"));
            Assert.That(Button("second input button").Id, Is.EqualTo("secondInputButtonId"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_id()
        {
            Assert.That(Button("firstInputButtonId").Value, Is.EqualTo("first input button"));
            Assert.That(Button("thirdInputButtonId").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_name()
        {
            Assert.That(Button("secondInputButtonId").Value, Is.EqualTo("second input button"));
            Assert.That(Button("thirdInputButtonName").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_value()
        {
            Assert.That(Button("first submit button").Id, Is.EqualTo("firstSubmitButtonId"));
            Assert.That(Button("second submit button").Id, Is.EqualTo("secondSubmitButtonId"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_id()
        {
            Assert.That(Button("firstSubmitButtonId").Value, Is.EqualTo("first submit button"));
            Assert.That(Button("thirdSubmitButtonId").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_name()
        {
            Assert.That(Button("secondSubmitButtonName").Value, Is.EqualTo("second submit button"));
            Assert.That(Button("thirdSubmitButtonName").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_image_buttons()
        {
            Assert.That(Button("firstImageButtonId").Value, Is.EqualTo("first image button"));
            Assert.That(Button("secondImageButtonId").Value, Is.EqualTo("second image button"));
        }

        [Test]
        public void Does_not_find_text_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstTextInputId"));
        }

        [Test]
        public void Does_not_find_email_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstEmailInputId"));
        }

        [Test]
        public void Does_not_find_tel_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstTelInputId"));
        }

        [Test]
        public void Does_not_find_url_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstUrlInputId"));
        }

        [Test]
        public void Does_not_find_hidden_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstHiddenInputId"));
        }

        [Test]
        public void Does_not_find_invisible_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => Button("firstInvisibleInputId"));
        }

        [Test]
        public void Finds_img_elements_with_role_button_by_alt_text()
        {
            Assert.That(Button("I'm an image with the role of button").Id, Is.EqualTo("roleImageButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_role_button_by_text()
        {
            Assert.That(Button("I'm a span with the role of button").Id, Is.EqualTo("roleSpanButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_class_button_by_text()
        {
            Assert.That(Button("I'm a span with the class of button").Id, Is.EqualTo("classButtonSpanButtonId"));
        }

        [Test]
        public void Finds_any_elements_with_class_btn_by_text()
        {
            Assert.That(Button("I'm a span with the class of btn").Id, Is.EqualTo("classBtnSpanButtonId"));
        }

        [Test]
        public void Finds_an_image_button_with_both_types_of_quote_in_my_value()
        {
            var button = Button("I'm an image button with \"both\" types of quote in my value");
            Assert.That(button.Id, Is.EqualTo("buttonWithBothQuotesId"));
        }
    }
}