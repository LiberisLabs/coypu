using System;
using System.Linq;

namespace Coypu
{
    /// <summary>
    /// Options for how Coypu interacts with the browser.
    /// </summary>
    public class Options
    {
        private const bool DefaultConsiderInvisibleElements = false;
        private const TextPrecision DefaultPrecision = TextPrecision.PreferExact;
        private const Match DefaultMatch = Match.Single;
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromSeconds(0.05);
        private static readonly TimeSpan DefaultWaitBeforeClick = TimeSpan.Zero;

        protected bool? considerInvisibleElements;
        private TextPrecision? _textPrecision;
        private Match? _match;
        private TimeSpan? _retryInterval;
        private TimeSpan? _timeout;
        private TimeSpan? _waitBeforeClick;


        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) || Equals((Options) obj));
        }

        /// <summary>
        /// Will not wait for asynchronous updates to the page
        /// </summary>
        public static Options NoWait = new Options
        {
            Timeout = TimeSpan.Zero
        };

        /// <summary>
        /// Include invisible elements when finding
        /// </summary>
        public static Options Invisible = new Options
        {
            ConsiderInvisibleElements = true
        };

        /// <summary>
        /// Just picks the first element that matches
        /// </summary>
        public static Options First => new Options {Match = Match.First};

        /// <summary>
        /// Raises an error if more than one element match
        /// </summary>
        public static Options Single => new Options {Match = Match.Single};

        /// <summary>
        /// Match by exact visible text
        /// </summary>
        public static Options Exact => new Options {TextPrecision = TextPrecision.Exact};

        /// <summary>
        /// Match by substring in visible text
        /// </summary>
        public static Options Substring => new Options {TextPrecision = TextPrecision.Substring};

        /// <summary>
        /// If multiple matches are found, some of which are exact, and some of which are not, then the first exactly matching element is returned
        /// </summary>
        public static Options PreferExact => new Options {TextPrecision = TextPrecision.PreferExact};

        /// <summary>
        /// Match exact visible text; Just picks the first element that matches
        /// </summary>
        public static Options FirstExact = Merge(First, Exact);

        /// <summary>
        /// Match substring in visible text; Just picks the first element that matches
        /// </summary>
        public static Options FirstSubstring = Merge(First, Substring);

        /// <summary>
        /// Prefer exact text matches to substring matches; Just picks the first element that matches
        /// </summary>
        public static Options FirstPreferExact = Merge(First, PreferExact);

        /// <summary>
        /// Match exact visible text; Raises an error if more than one element match
        /// </summary>
        public static Options SingleExact = Merge(Single, Substring);

        /// <summary>
        /// Match by substring in visible text; Raises an error if more than one element match
        /// </summary>
        public static Options SingleSubstring = Merge(Single, Substring);

        /// <summary>
        /// Prefer exact text matches to substring matches; Raises an error if more than one element match
        /// </summary>
        public static Options SinglePreferExact = Merge(Single, PreferExact);

        /// <summary>
        /// <para>When retrying, how long to wait for elements to appear or actions to complete without error.</para>
        /// <para>Default: 1sec</para>
        /// </summary>
        public TimeSpan Timeout
        {
            get { return _timeout ?? DefaultTimeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// <para>How long to wait between retries</para>
        /// <para>Default: 100ms</para>
        /// </summary>
        public TimeSpan RetryInterval
        {
            get { return _retryInterval ?? DefaultRetryInterval; }
            set { _retryInterval = value; }
        }

        /// <summary>
        /// <para>How long to wait between finding an element and clicking it.</para>
        /// <para>Default: zero</para>
        /// </summary>
        public TimeSpan WaitBeforeClick
        {
            get { return _waitBeforeClick ?? DefaultWaitBeforeClick; }
            set { _waitBeforeClick = value; }
        }

        /// <summary>
        /// <para>By default Coypu will exclude any invisible elements, this allows you to override that behaviour</para>
        /// <para>Default: true</para>
        /// </summary>
        public bool ConsiderInvisibleElements
        {
            get { return considerInvisibleElements ?? DefaultConsiderInvisibleElements; }
            set { considerInvisibleElements = value; }
        }

        /// <summary>
        /// <para>Whether to consider substrings when finding elements by text, or just an exact match.</para>
        /// </summary>
        public TextPrecision TextPrecision
        {
            get { return _textPrecision ?? DefaultPrecision; }
            set { _textPrecision = value; }
        }

        internal bool TextPrecisionExact => _textPrecision == TextPrecision.Exact;

        /// <summary>
        /// <para>With PreventAmbiguousMatches you can control whether Coypu should throw an exception when multiple elements match a query.</para>
        /// </summary>
        public Match Match
        {
            get { return _match ?? DefaultMatch; }
            set { _match = value; }
        }

        internal string BuildAmbiguousMessage(string queryDescription, int count)
        {
            var message = $@"Ambiguous match, found {count} elements matching {queryDescription}

Coypu does this by default from v2.0. Your options:

 * Look for something more specific";


            if (TextPrecision != TextPrecision.Exact)
                message += Environment.NewLine + " * Set the Options.TextPrecision option to Exact to exclude substring text matches";

            if (Match != Match.First)
                message += Environment.NewLine + " * Set the Options.Match option to Match.First to just take the first matching element";

            return message;
        }

        /// <summary>
        /// Merge any unset Options from another set of Options.
        /// </summary>
        /// <param name="preferredOptions">The preferred set of options</param>
        /// <param name="defaultOptions">Any unset preferred options will be copied from this</param>
        /// <returns>The new merged Options</returns>
        public static Options Merge(Options preferredOptions, Options defaultOptions)
        {
            preferredOptions = preferredOptions ?? new Options();
            defaultOptions = defaultOptions ?? new Options();

            return new Options
            {
                considerInvisibleElements = Default(preferredOptions.considerInvisibleElements, defaultOptions.considerInvisibleElements),
                _textPrecision = Default(preferredOptions._textPrecision, defaultOptions._textPrecision),
                _match = Default(preferredOptions._match, defaultOptions._match),
                _retryInterval = Default(preferredOptions._retryInterval, defaultOptions._retryInterval),
                _timeout = Default(preferredOptions._timeout, defaultOptions._timeout),
                _waitBeforeClick = Default(preferredOptions._waitBeforeClick, defaultOptions._waitBeforeClick)
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected static T? Default<T>(T? value, T? defaultValue) where T : struct
        {
            return value ?? defaultValue;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                               GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).Select(p => p.Name + ": " + p.GetValue(this, null)).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Options other)
        {
            return considerInvisibleElements.Equals(other.considerInvisibleElements) && _textPrecision.Equals(other._textPrecision) && _match == other._match && _retryInterval.Equals(other._retryInterval) &&
                   _timeout.Equals(other._timeout) && _waitBeforeClick.Equals(other._waitBeforeClick);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = considerInvisibleElements.GetHashCode();
                hashCode = (hashCode*397) ^ _textPrecision.GetHashCode();
                hashCode = (hashCode*397) ^ _match.GetHashCode();
                hashCode = (hashCode*397) ^ _retryInterval.GetHashCode();
                hashCode = (hashCode*397) ^ _timeout.GetHashCode();
                hashCode = (hashCode*397) ^ _waitBeforeClick.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Options left, Options right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Options left, Options right)
        {
            return !Equals(left, right);
        }
    }
}