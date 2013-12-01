using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Metrics.Core
{
    public class FileParser
    {
        public String FilePath { get; set; }

        public FileParser(String filePath)
        {
            FilePath = File.Exists(filePath) ? filePath : null;
        }

        public String GetContent()
        {
            if (FilePath != null)
            {
                using (var stream = File.OpenRead(FilePath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return RemoveSingleLineComments(RemoveMultirowComments(reader.ReadToEnd()));
                    }
                }
            }
            return String.Empty;
        }

        private String RemoveMultirowComments(String source)
        {
            return JavaDictionary.MultirowCommentDeclaration.Replace(source, String.Empty);
        }

        private String RemoveSingleLineComments(String source)
        {
            return JavaDictionary.CommentDeclaration.Replace(source, String.Empty);
        }
    }
}
