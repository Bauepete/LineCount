using System;

namespace LinesCount
{
    public class SourceFile
    {
        private string[] lines;

        /// <summary>
        /// Gets the solution where the source file is located.
        /// </summary>
        /// <value>The in solution.</value>
        public string InSolution { get; private set; }

        /// <summary>
        /// Gets the project where the source file is located.
        /// </summary>
        /// <value>The in project.</value>
        public string InProject { get; private set; }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { private set; get; }

        /// <summary>
        /// Gets the physical lines of code, i.e., all lines including empty lines and comment lines
        /// </summary>
        /// <value>The lines of code.</value>
        public int LinesOfCode { private set; get; }

        /// <summary>
        /// Gets the source lines of code, i.e., all lines excluding empty lines and comment lines
        /// </summary>
        /// <value>The source lines of code.</value>
        public int SourceLinesOfCode { private set; get; }

        /// <summary>
        /// Gets the comment lines.
        /// </summary>
        /// <value>The comment lines.</value>
        public int CommentLines { private set; get; }

        /// <summary>
        /// Gets the effective lines of code.
        /// </summary>
        /// <value>The effective lines of code.</value>
        public int EffectiveLinesOfCode { private set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinesCount.SourceFile"/> class.
        /// </summary>
        /// <param name="inSolution">Solution in which the source file is contained.</param>
        /// <param name="inProject">Project in which the source file is contained.</param>
        /// <param name="filePath">File path.</param>
        /// <param name="lines">Lines.</param>
        public SourceFile(string inSolution, string inProject, string filePath, string[] lines)
        {
            this.InSolution = inSolution;
            this.InProject = inProject;
            this.FilePath = filePath;
            this.lines = lines;

            LinesOfCode = -1;
            SourceLinesOfCode = -1;
            EffectiveLinesOfCode = -1;
            CommentLines = -1;
        }
            
        /// <summary>
        /// Triggers to get the source file analyzed by a source line analyzer.
        /// </summary>
        /// <param name="sourceLineAnalyzer">Source line analyzer.</param>
        public void GetAnalyzedBy(ISourceLineAnalyzer sourceLineAnalyzer)
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

