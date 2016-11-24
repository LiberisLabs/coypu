using System;
using System.Collections.Generic;
using System.Linq;

namespace Coypu.Queries
{
    internal class FindAllCssWithPredicateQuery : DriverScopeQuery<IEnumerable<SnapshotElementScope>>
    {
        private readonly string _locator;
        private readonly Func<IEnumerable<SnapshotElementScope>, bool> _predicate;

        public FindAllCssWithPredicateQuery(string locator, Func<IEnumerable<SnapshotElementScope>, bool> predicate, DriverScope driverScope, Options options) : base(driverScope, options)
        {
            if (predicate == null)
                predicate = e => true;

            _predicate = predicate;
            _locator = locator;
        }

        public override IEnumerable<SnapshotElementScope> Run()
        {
            var allElements = Scope.FindAllCssNoPredicate(_locator, Options).ToArray();
            if (!_predicate(allElements))
                throw new MissingHtmlException("FindAllCss did not find elements matching your predicate");

            return allElements;
        }

        public override object ExpectedResult => null;
    }
}