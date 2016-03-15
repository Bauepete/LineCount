using System;

namespace LinesCount
{
    public class SourceFile
    {
        public string FilePath { private set; get; }

        /// <summary>
        /// Gets the physical lines of code, i.e., all lines including empty lines and comments
        /// </summary>
        /// <value>The lines of code.</value>
        public int LinesOfCode { set; get; }

        public int SourceLinesOfCode { set; get; }

        public int CommentLines { private set; get; }

        public int EffectiveLinesOfCode { private set; get; }

        public SourceFile(string filePath, string[] lines)
        {

        }

        [Obsolete ("Three parameter constructor will be dropped")]
        public SourceFile(string filePath, string[] lines, ISourceLineAnalyzer sla)
        {
            LinesOfCode = lines.Length;
            foreach (string line in lines)
            {
                if (sla.IsSourceLine(line))
                    SourceLinesOfCode++;
                if (sla.IsCommentLine(line))
                    CommentLines++;
                if (sla.IsEffectiveCodeLine(line))
                    EffectiveLinesOfCode++;
            }
        }

        public void Analyze(ISourceLineAnalyzer sourceLineAnalyzer)
        {

        }

    }
}

