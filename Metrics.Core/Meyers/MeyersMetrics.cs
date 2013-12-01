using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Metrics.Core
{
    public class MeyersMetrics
    {
        public int CyclomaticComplexity { get; private set; }

        public int PredicateDepth { get; private set; }

        private int returnPaths { get; private set; }

        public void Initialize(String source)
        {
            SetComplexity(source);
            SetPredicateDepth(source);
        }

        private void SetComplexity(String source)
        {
            CyclomaticComplexity = JavaDictionary.CyclomaticsOperators.Matches(source).Count + 1;
        }

        private void SetPredicateDepth(String source)
        {
            var collection = JavaDictionary.IfOperator.Matches(source);
            foreach (var condition in collection)
            {
                int predicateCounts = JavaDictionary.BooleanOperators.Matches(condition.ToString()).Count;
                PredicateDepth = Math.Max(PredicateDepth, predicateCounts);
            }
        }
    }
}
