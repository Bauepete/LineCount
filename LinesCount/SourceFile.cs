using System;

namespace LinesCount
{
    public class SourceFile
    {
        private string[] lines;

        public string FilePath { private set; get; }

        /// <summary>
        /// Gets the physical lines of code, i.e., all lines including empty lines and comments
        /// </summary>
        /// <value>The lines of code.</value>
        public int LinesOfCode { private set; get; }

        public int SourceLinesOfCode { private set; get; }

        public int CommentLines { private set; get; }

        public int EffectiveLinesOfCode { private set; get; }

        public SourceFile(string filePath, string[] lines)
        {
            this.FilePath = filePath;
            this.lines = lines;

            LinesOfCode = -1;
            SourceLinesOfCode = -1;
            EffectiveLinesOfCode = -1;
            CommentLines = -1;
        }
            
        public void GetAnalyzed(ISourceLineAnalyzer sourceLineAnalyzer)
        {
            LinesOfCode = 0;
            SourceLinesOfCode = 0;
            CommentLines = 0;
            EffectiveLinesOfCode = 0;
            foreach (string line in lines)
            {
                LinesOfCode++;
                if (sourceLineAnalyzer.IsSourceLine(line))
                    SourceLinesOfCode++;
                if (sourceLineAnalyzer.IsCommentLine(line))
                    CommentLines++;
                if (sourceLineAnalyzer.IsEffectiveCodeLine(line))
                    EffectiveLinesOfCode++;
            }
        }

    }
}

