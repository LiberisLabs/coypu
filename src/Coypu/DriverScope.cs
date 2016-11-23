using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Coypu.Actions;
using Coypu.Drivers;
using Coypu.Finders;
using Coypu.Queries;
using Coypu.Timing;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DriverScope : Scope
    {
        protected readonly SessionConfiguration SessionConfiguration;
        internal readonly ElementFinder elementFinder;

        protected Driver driver;
        protected TimingStrategy timingStrategy;
        protected readonly Waiter waiter;
        internal UrlBuilder urlBuilder;
        internal StateFinder stateFinder;
        private Element element;
        protected readonly IDisambiguationStrategy DisambiguationStrategy = new FinderOptionsDisambiguationStrategy();

        internal DriverScope(SessionConfiguration sessionConfiguration, ElementFinder elementFinder, Driver driver, TimingStrategy timingStrategy, Waiter waiter, UrlBuilder urlBuilder,
                             IDisambiguationStrategy disambiguationStrategy)
        {
            this.elementFinder = elementFinder ?? new DocumentElementFinder(driver, sessionConfiguration);
            SessionConfiguration = sessionConfiguration;
            this.driver = driver;
            this.timingStrategy = timingStrategy;
            this.waiter = waiter;
            this.urlBuilder = urlBuilder;
            DisambiguationStrategy = disambiguationStrategy;
            stateFinder = new StateFinder(timingStrategy);
        }

        internal DriverScope(ElementFinder elementFinder, DriverScope outerScope)
        {
            this.elementFinder = elementFinder;
            OuterScope = outerScope;
            driver = outerScope.driver;
            timingStrategy = outerScope.timingStrategy;
            urlBuilder = outerScope.urlBuilder;
            DisambiguationStrategy = outerScope.DisambiguationStrategy;
            stateFinder = outerScope.stateFinder;
            waiter = outerScope.waiter;
            SessionConfiguration = outerScope.SessionConfiguration;
        }

        /// <summary>
        /// 
        /// </summary>
        public DriverScope OuterScope { get; }

        /// <inheritdoc />
        public virtual Uri Location => driver.Location(this);

        /// <summary>
        /// 
        /// </summary>
        public string Text => Now().Text;

        /// <summary>
        /// 
        /// </summary>
        public Browser Browser => SessionConfiguration.Browser;

        /// <summary>
        /// 
        /// </summary>
        public ElementFinder ElementFinder => elementFinder;

        internal Options Merge(Options options)
        {
            var mergeWith = ElementFinder != null ? ElementFinder.Options : SessionConfiguration;
            return Options.Merge(options, mergeWith);
        }

        internal abstract bool Stale { get; set; }

        public void ClickButton(string locator, Options options = null)
        {
            RetryUntilTimeout(WaitThenClickButton(locator, Merge(options)));
        }

        public void ClickLink(string locator, Options options = null)
        {
            RetryUntilTimeout(WaitThenClickLink(locator, Merge(options)));
        }

        private WaitThenClick WaitThenClickLink(string locator, Options options = null)
        {
            return new WaitThenClick(driver, this, Merge(options), waiter, new LinkFinder(driver, locator, this, Merge(options)), DisambiguationStrategy);
        }

        private WaitThenClick WaitThenClickButton(string locator, Options options = null)
        {
            return new WaitThenClick(driver, this, Merge(options), waiter, new ButtonFinder(driver, locator, this, Merge(options)), DisambiguationStrategy);
        }

        /// <inheritdoc />
        public Scope ClickButton(string locator, PredicateQuery until, Options options = null)
        {
            TryUntil(WaitThenClickButton(locator, Merge(options)), until, Merge(options));
            return this;
        }

        /// <inheritdoc />
        public Scope ClickLink(string locator, PredicateQuery until, Options options = null)
        {
            TryUntil(WaitThenClickLink(locator, Merge(options)), until, Merge(options));
            return this;
        }

        /// <inheritdoc />
        public ElementScope FindButton(string locator, Options options = null)
        {
            return new ButtonFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindLink(string locator, Options options = null)
        {
            return new LinkFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindField(string locator, Options options = null)
        {
            return new FieldFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public FillInWith FillIn(string locator, Options options = null)
        {
            return new FillInWith(FindField(locator, options), driver, timingStrategy, Merge(options));
        }

        /// <inheritdoc />
        public SelectFrom Select(string option, Options options = null)
        {
            return new SelectFrom(option, driver, timingStrategy, this, Merge(options), DisambiguationStrategy);
        }

        /// <inheritdoc />
        public bool HasContent(string text, Options options = null)
        {
            return Query(new HasContentQuery(this, text, Merge(options)));
        }

        /// <inheritdoc />
        public bool HasContentMatch(Regex pattern, Options options = null)
        {
            return Query(new HasContentMatchQuery(this, pattern, Merge(options)));
        }

        /// <inheritdoc />
        public bool HasNoContent(string text, Options options = null)
        {
            return Query(new HasNoContentQuery(this, text, Merge(options)));
        }

        /// <inheritdoc />
        public bool HasNoContentMatch(Regex pattern, Options options = null)
        {
            return Query(new HasNoContentMatchQuery(this, pattern, Merge(options)));
        }

        /// <inheritdoc />
        public ElementScope FindCss(string cssSelector, Options options = null)
        {
            return new CssFinder(driver, cssSelector, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindCss(string cssSelector, string text, Options options = null)
        {
            return new CssFinder(driver, cssSelector, this, Merge(options), text).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindCss(string cssSelector, Regex text, Options options = null)
        {
            return new CssFinder(driver, cssSelector, this, Merge(options), text).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindXPath(string xpath, Options options = null)
        {
            return new XPathFinder(driver, xpath, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindXPath(string xpath, string text, Options options = null)
        {
            return new XPathFinder(driver, xpath, this, Merge(options), text).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindXPath(string xpath, Regex text, Options options = null)
        {
            return new XPathFinder(driver, xpath, this, Merge(options), text).AsScope();
        }

        /// <inheritdoc />
        public IEnumerable<SnapshotElementScope> FindAllCss(string cssSelector, Func<IEnumerable<SnapshotElementScope>, bool> predicate = null, Options options = null)
        {
            return Query(new FindAllCssWithPredicateQuery(cssSelector, predicate, this, Merge(options)));
        }

        internal IEnumerable<SnapshotElementScope> FindAllCssNoPredicate(string cssSelector, Options options)
        {
            return driver.FindAllCss(cssSelector, this, options).AsSnapshotElementScopes(this, options);
        }

        /// <inheritdoc />
        public IEnumerable<SnapshotElementScope> FindAllXPath(string xpath, Func<IEnumerable<SnapshotElementScope>, bool> predicate = null, Options options = null)
        {
            return Query(new FindAllXPathWithPredicateQuery(xpath, predicate, this, Merge(options)));
        }

        internal IEnumerable<SnapshotElementScope> FindAllXPathNoPredicate(string xpath, Options options)
        {
            return driver.FindAllXPath(xpath, this, options).AsSnapshotElementScopes(this, options);
        }

        /// <inheritdoc />
        public ElementScope FindSection(string locator, Options options = null)
        {
            return new SectionFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindFieldset(string locator, Options options = null)
        {
            return new FieldsetFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindId(string id, Options options = null)
        {
            return new IdFinder(driver, id, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public ElementScope FindIdEndingWith(string endsWith, Options options = null)
        {
            return FindCss($@"*[id$=""{endsWith}""]", options);
        }

        /// <inheritdoc />
        public void Check(string locator, Options options = null)
        {
            RetryUntilTimeout(new CheckAction(driver, FindField(locator, options), Merge(options)));
        }

        /// <inheritdoc />
        public void Uncheck(string locator, Options options = null)
        {
            RetryUntilTimeout(new Uncheck(driver, FindField(locator, options), Merge(options)));
        }

        /// <inheritdoc />
        public void Choose(string locator, Options options = null)
        {
            RetryUntilTimeout(new Choose(driver, FindField(locator, options), Merge(options)));
        }

        /// <inheritdoc />
        public void RetryUntilTimeout(Action action, Options options = null)
        {
            timingStrategy.Synchronise(new LambdaBrowserAction(action, Merge(options)));
        }

        /// <inheritdoc />
        public TResult RetryUntilTimeout<TResult>(Func<TResult> function, Options options = null)
        {
            return timingStrategy.Synchronise(new LambdaQuery<TResult>(function, Merge(options)));
        }

        /// <inheritdoc />
        public void RetryUntilTimeout(BrowserAction action)
        {
            Query(action);
        }

        /// <inheritdoc />
        public ElementScope FindFrame(string locator, Options options = null)
        {
            return new FrameFinder(driver, locator, this, Merge(options)).AsScope();
        }

        /// <inheritdoc />
        public T Query<T>(Func<T> query, T expecting, Options options = null)
        {
            return timingStrategy.Synchronise(new LambdaQuery<T>(query, expecting, Merge(options)));
        }

        /// <inheritdoc />
        public T Query<T>(Query<T> query)
        {
            return timingStrategy.Synchronise(query);
        }

        /// <inheritdoc />
        public void TryUntil(Action tryThis, Func<bool> until, TimeSpan waitBeforeRetry, Options options = null)
        {
            var mergedOptions = Merge(options);
            var predicateOptions = Options.Merge(new Options {Timeout = waitBeforeRetry}, mergedOptions);

            timingStrategy.TryUntil(new LambdaBrowserAction(tryThis, mergedOptions),
                                    new LambdaPredicateQuery(WithZeroTimeout(until), predicateOptions), mergedOptions);
        }

        private Func<bool> WithZeroTimeout(Func<bool> query)
        {
            var zeroTimeoutUntil = new Func<bool>(() =>
            {
                var was = timingStrategy.ZeroTimeout;
                timingStrategy.ZeroTimeout = true;
                try
                {
                    return query();
                }
                finally
                {
                    timingStrategy.ZeroTimeout = was;
                }
            });
            return zeroTimeoutUntil;
        }

        /// <inheritdoc />
        public void TryUntil(BrowserAction tryThis, PredicateQuery until, Options options = null)
        {
            timingStrategy.TryUntil(tryThis, until, Merge(options));
        }

        /// <inheritdoc />
        public State FindState(State[] states, Options options = null)
        {
            return stateFinder.FindState(states, this, Merge(options));
        }

        /// <inheritdoc />
        public State FindState(params State[] states)
        {
            return FindState(states, null);
        }

        /// <summary>
        /// Try and find this scope now
        /// </summary>
        /// <returns></returns>
        /// <exception cref="T:Coypu.MissingHtmlException">Thrown if the element cannot be found</exception>
        /// <exception cref="T:Coypu.AmbiguousHtmlException">Thrown if the there is more than one matching element and the Match.Single option is set</exception>
        public virtual Element Now()
        {
            return FindElement();
        }

        protected internal virtual Element FindElement()
        {
            if (element == null || Stale)
                element = DisambiguationStrategy.ResolveQuery(ElementFinder);
            return element;
        }
    }
}