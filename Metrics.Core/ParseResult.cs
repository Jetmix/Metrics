using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Core
{
    public class ParseResult
    {
        public ParseResult()
        {
            Methods = new List<String>();
            Variables = new List<String>();
        }

        public ICollection<String> Methods { get; set; }

        public ICollection<String> Variables { get; set; }
    }
}
