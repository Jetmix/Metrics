using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Metrics.Core
{
    public class ChepinMetrics
    {
        private List<Variable> Variables = new List<Variable>();

        private double a4 = 0.5;

        private List<String> Initialize(IEnumerable<String> source)
        {
            List<String> executionLines = new List<String>();
            foreach (var line in source)
            {
                if (JavaDictionary.Variable.IsMatch(line))
                {
                    Variables.Add(new Variable(JavaDictionary.Variable.Match(line).Groups["name"].Value));
                }
                else
                {
                    executionLines.Add(line);
                }
            }
            return executionLines;
        }

        private void Group(IEnumerable<String> source)
        {
            foreach (var line in source)
            {
                foreach (var variable in Variables)
                {
                    CheckVariable(line, variable);
                }
            }
        }

        private void CheckVariable(String line, Variable variable)
        {
            AddVariable(line, JavaDictionary.OutputVariable, variable, VariableTypes.Output);
            AddVariable(line, JavaDictionary.ChangedVariable, variable, VariableTypes.Modify);
            AddVariable(line, JavaDictionary.ControlVariable, variable, VariableTypes.Control);
        }

        private void AddVariable(String line, String template, Variable variable, VariableTypes type)
        {
            if (new Regex(String.Format(template, variable.Name)).IsMatch(line))
            {
                variable.AddType(type);
            }
        }

        private void AddStrings(String source)
        {
            var matches = new Regex(JavaDictionary.StringQuotes).Matches(source);
            foreach (var str in matches)
            {
                var added = new Variable(((Match)str).Value);
                added.AddType(VariableTypes.Output);
                Variables.Add(added);
            }
        }

        public double Calculate(String source)
        {
            AddStrings(source);
            String[] codeLines = String.Join(String.Empty, new Regex(JavaDictionary.StringQuotes).Split(source)).Split('\n'); 
            IEnumerable<String> executionLines = Initialize(codeLines);
            Group(executionLines);
            return GetIndex();
        }

        private double GetIndex()
        {
            double Result = 0;
            foreach (var variable in Variables)
            {
                if (variable.Type.Count == 0)
                {
                    Result += a4;
                }
                else
                {
                    variable.Type.ForEach(x => Result += (int)x);
                }
            }
            return Result;
        }

        public String GetAllVariables()
        {
            String result = String.Empty;
            Variables.ForEach(x => result += x.ToString() + "\n");
            return result;
        }
    }
}
