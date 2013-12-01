using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Metrics.Core
{
    public class JilbMetrics
    {
        public int TotalOperators { get; private set; }

        public int ConditionalOperators { get; private set; }

        public void Initialize(String source)
        {
            source = String.Join(String.Empty, new Regex(JavaDictionary.StringQuotes).Split(source));
            ConditionalOperatorsCount(source);
            TotalOperatorsCount(source);
        }

        private void ConditionalOperatorsCount(String source)
        {
            ConditionalOperators = JavaDictionary.ConditionalOperators.Matches(source).Count;
        }

        private void TotalOperatorsCount(String source)
        {
            TotalOperators = 0;
            foreach (var operatorName in JavaDictionary.Operators)
            {
                String name = String.Empty;
                foreach (var letter in operatorName)
                {
                    name += "\\" + letter;
                }
                TotalOperators += new Regex(name).Matches(source).Count;
                source = String.Join(String.Empty, new Regex(name).Split(source));
            }
            TotalOperators += ConditionalOperators;
        }
    }
}
