using System;
using Coypu.Actions;
using Coypu.Queries;

namespace Coypu
{
    /// <summary>
    /// The scope of an element already found in the document, therefore not deferred. 
    /// 
    /// If this element becomes stale then using this scope will not try to refind the element but 
    /// will raise a MissingHtmlException immediately.
    /// </summary>
    public class SnapshotElementScope : ElementScope
    {
        private readonly IElement _element;

        internal SnapshotElementScope(IElement element, DriverScope scope, Options options)
            : base(null, scope)
        {
            _element = element;
        }

        internal override bool Stale
        {
            get { return true; }
            set { }
        }

        protected internal override IElement FindElement() => _element;

        internal override void Try(DriverAction action)
        {
            action.Act();
        }

        internal override bool Try(IQuery<bool> query)
        {
            return query.Run();
        }

        internal override T Try<T>(Func<T> getAttribute)
        {
            return getAttribute();
        }

        /// <inheritdoc />
        public override bool Exists(Options o = null)
        {
            return FindXPath(".", o).Exists();
        }

        /// <inheritdoc />
        public override bool Missing(Options o = null)
        {
            return FindXPath(".", o).Missing();
        }
    }
}