using System.Linq;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class SeleniumElement : IElement
    {
        protected readonly IWebElement native;
        protected readonly IWebDriver selenium;

        public SeleniumElement(IWebElement seleniumElement, IWebDriver selenium)
        {
            native = seleniumElement;
            this.selenium = selenium;
        }

        public string Id => native.GetAttribute("id");

        public virtual string Text => native.Text;

        public string Value => native.GetAttribute("value");

        public string Name => native.GetAttribute("name");

        public virtual string OuterHtml => native.GetAttribute("outerHTML");

        public virtual string InnerHtml => native.GetAttribute("innerHTML");

        public string Title => native.GetAttribute("title");

        public bool Disabled => !native.Enabled;

        public string SelectedOption
        {
            get
            {
                return native.FindElements(By.TagName("option"))
                             .Where(e => e.Selected)
                             .Select(e => e.Text)
                             .FirstOrDefault();
            }
        }

        public bool Selected => native.Selected;

        public virtual object Native => native;

        public string this[string attributeName] => native.GetAttribute(attributeName);
    }
}