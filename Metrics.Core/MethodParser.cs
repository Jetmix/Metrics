using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Core
{
    public class MethodParser
    {
        private readonly char OpenBracket = '{';

        private readonly char CloseBracket = '}';

        public ParseResult Parse(String source)
        {
            var parseResult = new ParseResult();
            var lines = GetCodeLines(source);
            int brackets = 0, index = 0;
            while (index < lines.Length)
            {
                if (JavaDictionary.MethodDeclaration.IsMatch(lines[index]))
                {
                    String methodBody = String.Empty;
                    while (!lines[index].Contains(OpenBracket))
                    {
                        methodBody += lines[index] + "\r\n";
                        index++;
                    }
                    do 
                    {
                        brackets += lines[index].Count(x => x == OpenBracket);
                        brackets -= lines[index].Count(x => x == CloseBracket);
                        methodBody += lines[index] + "\r\n";
                        index++;
                    } while (brackets != 0);
                    index--;
                    parseResult.Methods.Add(methodBody);
                }
                if (JavaDictionary.FieldDeclaration.IsMatch(lines[index]))
                {
                    parseResult.Variables.Add(lines[index]);
                }
                index++;
            }
            return parseResult;
        }

        private String[] GetCodeLines(String source)
        {
            return source.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
    }
}
