using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenFindingButtons
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_a_particular_button_by_its_text()
        {
            Assert.That(DriverHelpers.Button(_driver, "first button").Id, Is.EqualTo("firstButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "second button").Id, Is.EqualTo("secondButtonId"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstButtonId").Text, Is.EqualTo("first button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdButtonId").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(_driver, "secondButtonName").Text, Is.EqualTo("second button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdButtonName").Text, Is.EqualTo("third button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_value()
        {
            Assert.That(DriverHelpers.Button(_driver, "first input button").Id, Is.EqualTo("firstInputButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "second input button").Id, Is.EqualTo("secondInputButtonId"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstInputButtonId").Value, Is.EqualTo("first input button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdInputButtonId").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_input_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(_driver, "secondInputButtonId").Value, Is.EqualTo("second input button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdInputButtonName").Value, Is.EqualTo("third input button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_value()
        {
            Assert.That(DriverHelpers.Button(_driver, "first submit button").Id, Is.EqualTo("firstSubmitButtonId"));
            Assert.That(DriverHelpers.Button(_driver, "second submit button").Id, Is.EqualTo("secondSubmitButtonId"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_id()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstSubmitButtonId").Value, Is.EqualTo("first submit button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdSubmitButtonId").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_a_particular_submit_button_by_its_name()
        {
            Assert.That(DriverHelpers.Button(_driver, "secondSubmitButtonName").Value, Is.EqualTo("second submit button"));
            Assert.That(DriverHelpers.Button(_driver, "thirdSubmitButtonName").Value, Is.EqualTo("third submit button"));
        }

        [Test]
        public void Finds_image_buttons()
        {
            Assert.That(DriverHelpers.Button(_driver, "firstImageButtonId").Value, Is.EqualTo("first image button"));
            Assert.That(DriverHelpers.Button(_driver, "secondImageButtonId").Value, Is.EqualTo("second image button"));
        }

        [Test]
        public void Does_not_find_text_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstTextInputId"));
        }

        [Test]
        public void Does_not_find_email_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstEmailInputId"));
        }

        [Test]
        public void Does_not_find_tel_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstTelInputId"));
        }

        [Test]
        public void Does_not_find_url_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstUrlInputId"));
        }

        [Test]
        public void Does_not_find_hidden_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstHiddenInputId"));
        }

        [Test]
        public void Does_not_find_invisible_inputs()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "firstInvisibleInputId"));
        }

        [Test]
        public void Finds_img_elements_with_role_button_by_alt_text()
        {
            Assert.That("roleImageButtonId", Is.EqualTo(DriverHelpers.Button(_driver, "I'm an image with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_role_button_by_text()
        {
            Assert.That("roleSpanButtonId", Is.EqualTo(DriverHelpers.Button(_driver, "I'm a span with the role of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_button_by_text()
        {
            Assert.That("classButtonSpanButtonId", Is.EqualTo(DriverHelpers.Button(_driver, "I'm a span with the class of button").Id));
        }

        [Test]
        public void Finds_any_elements_with_class_btn_by_text()
        {
            Assert.That("classBtnSpanButtonId", Is.EqualTo(DriverHelpers.Button(_driver, "I'm a span with the class of btn").Id));
        }

        [Test]
        public void Finds_an_image_button_with_both_types_of_quote_in_my_value()
        {
            var button = DriverHelpers.Button(_driver, "I'm an image button with \"both\" types of quote in my value");
            Assert.That("buttonWithBothQuotesId", Is.EqualTo(button.Id));
        }
    }
}