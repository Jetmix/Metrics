using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Metrics.Core
{
    public static class JavaDictionary
    {
        #region Chepin
        public static Regex Variable = new Regex(@"(\b\w+([\[\]\<\>\w]*)?\s*){1,2}(\b(?<name>\w+)\b)(\s*=\s*.*?)?;");

        public static String OutputVariable = @".*\Wprint.*\W{0}\W";

        public static String ControlVariable = @"(.*if.*\W{0}\W)|(.*for.*\W{0}\W)|(.*while.*\W{0}\W)|(.*\W{0}\.)|(\w+\(.*{0}.*\);)";

        public static String ChangedVariable = @".*\W{0}\W.*=";
        #endregion

        #region Jilb
        public static String StringQuotes = @""".*?""";

        public static List<String> Operators = new List<String>(){".", "~", "!", "*", "/", "%", "+", "-", ">>",	">>>", "<<",	 
">", ">=", "<", "<=", "==",	"!=", "&", "^", "|", "&&", "||", "=", "+=", "-=", "*=", "/=", "%=", "++", "--" }.OrderByDescending(x => x.Length).ToList();

        public static Regex ConditionalOperators = new Regex(@"(if)|(.*\?.*\:.*;)|(for)|(while)");
        #endregion

        #region Meyers
        public static Regex BooleanOperators = new Regex(@"(\&\&)|(\|\|)|(\|)|(\&)");

        public static Regex CyclomaticsOperators = new Regex(@"(if)|(for)|(while)|(case)");

        public static Regex IfOperator = new Regex(@"if.*?\(.*?\)");
        #endregion

        #region General

        public static Regex MethodDeclaration = new Regex(@"(public|protected|private).*\).*{", RegexOptions.Singleline);

        public static Regex FieldDeclaration = new Regex(@"^(\s*\b\w+\b){2,3}(\s*=\s*.*)?.*;");

        public static Regex ClassDeclaration = new Regex(@"(public|protected|private)?\s*class");

        public static Regex CommentDeclaration = new Regex(@"\/\/.*\r\n");

        public static Regex MultirowCommentDeclaration = new Regex(@"\/\*.*?\*\/", RegexOptions.Singleline);

        #endregion
    }
}

