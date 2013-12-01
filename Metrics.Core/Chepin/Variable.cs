using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Core
{
    public class Variable
    {
        public String Name { get; set; }

        public int Count { get; set; }

        public List<VariableTypes> Type { get; set; }

        public Variable(String name)
        {
            Name = name;
            Count = 1;
            Type = new List<VariableTypes>();
        }

        public void AddType(VariableTypes type)
        {
            if (!Type.Any(x => x == type))
            {
                Type.Add(type);
            }
        }

        public override string ToString()
        {
            String type = String.Empty;
            Type.ForEach(x => type += x.ToString() + ", ");
            return String.Format("{0} ({1})", Name, type);
        }
    }
}
