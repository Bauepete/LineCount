using System;

namespace LinesCount
{
    public class SourceFile
    {
        /// <summary>
        /// Gets the physical lines of code, i.e., all lines including empty lines and comments
        /// </summary>
        /// <value>The lines of code.</value>
        public int LinesOfCode { private set; get; }

        public int SourceLinesOfCode { private set; get; }

        public int CommentLines { private set; get; }

        public int EffectiveLinesOfCode { private set; get; }

        public SourceFile(string name, string[] lines, ISourceLineAnalyzer sla)
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

    }
}

