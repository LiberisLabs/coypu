using System;
using Coypu.Actions;
using Coypu.Finders;
using Coypu.Queries;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ElementScope : DriverScope, IElement
    {
        internal ElementScope(ElementFinder elementFinder, DriverScope outerScope)
            : base(elementFinder, outerScope)
        {
        }

        internal abstract void Try(DriverAction action);
        internal abstract bool Try(IQuery<bool> query);
        internal abstract T Try<T>(Func<T> getAttribute);

        /// <inheritdoc />
        public string Id
        {
            get { return Try(() => Now().Id); }
        }

        /// <inheritdoc />
        public new string Text
        {
            get { return Try(() => Now().Text); }
        }

        /// <inheritdoc />
        public string Value
        {
            get { return Try(() => Now().Value); }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return Try(() => Now().Name); }
        }

        /// <inheritdoc />
        public string OuterHtml
        {
            get { return Try(() => Now().OuterHtml); }
        }

        /// <inheritdoc />
        public string InnerHtml
        {
            get { return Try(() => Now().InnerHtml); }
        }

        /// <inheritdoc />
        public string Title
        {
            get { return Try(() => Now().Title); }
        }

        /// <inheritdoc />
        public string SelectedOption
        {
            get { return Try(() => Now().SelectedOption); }
        }

        /// <inheritdoc />
        public bool Selected
        {
            get { return Try(() => Now().Selected); }
        }

        /// <inheritdoc />
        public object Native
        {
            get { return Try(() => Now().Native); }
        }

        /// <inheritdoc />
        public bool Disabled
        {
            get { return Try(() => Now().Disabled); }
        }

        /// <inheritdoc />
        public string this[string attributeName]
        {
            get { return Try(() => Now()[attributeName]); }
        }

        /// <inheritdoc />
        public ElementScope Click(Options options = null)
        {
            Try(new ClickAction(this, driver, Merge(options)));
            return this;
        }

        /// <summary>
        /// Treat this scope as an input field and fill in with the specified value
        /// </summary>
        /// <param name="value">The value to fill in with</param>
        /// <param name="options">
        /// <para>Override the way Coypu is configured to find elements for this call only.</para>
        /// <para>E.g. A longer wait:</para></param>
        /// <returns>The current scope</returns>
        public ElementScope FillInWith(string value, Options options = null)
        {
            Try(new FillIn(driver, this, value, Merge(options)));
            return this;
        }

        /// <summary>
        /// Treat this scope as a select element and choose the specified option
        /// </summary>
        /// <param name="value">The text or value of the option</param>
        /// <param name="options">
        /// <para>Override the way Coypu is configured to find elements for this call only.</para>
        /// <para>E.g. A longer wait</para></param>
        /// <returns>The current scope</returns>
        public ElementScope SelectOption(string value, Options options = null)
        {
            Try(new Select(driver, this, value, DisambiguationStrategy, Merge(options)));
            return this;
        }

        /// <inheritdoc />
        public ElementScope Hover(Options options = null)
        {
            Try(new Hover(this, driver, Merge(options)));
            return this;
        }

        /// <inheritdoc />
        public ElementScope SendKeys(string keys, Options options = null)
        {
            Try(new SendKeys(keys, this, driver, Merge(options)));
            return this;
        }

        /// <inheritdoc />
        public ElementScope Check(Options options = null)
        {
            Try(new CheckAction(driver, this, Merge(options)));
            return this;
        }

        /// <inheritdoc />
        public ElementScope Uncheck(Options options = null)
        {
            Try(new Uncheck(driver, this, Merge(options)));
            return this;
        }

        /// <inheritdoc />
        public abstract bool Exists(Options options = null);

        /// <inheritdoc />
        public abstract bool Missing(Options options = null);

        /// <inheritdoc />
        public bool HasValue(string text, Options options = null)
        {
            return Try(new HasValueQuery(this, text, Merge(options)));
        }

        /// <inheritdoc />
        public bool HasNoValue(string text, Options options = null)
        {
            return Try(new HasNoValueQuery(this, text, Merge(options)));
        }
    }
}