using System;
using System.Collections.Generic;

namespace LinesCount
{
    public class LinesCounter
    {
        private ISourceLineAnalyzer sourceLineAnalyzer;

        public class Result
        {
            public struct OverallResult
            {
                public int TotalLines;
                public int SourceLines;
                public int EffectiveLines;
                public int CommentLines;
            }

            public Result.OverallResult Overall { get; set; }

            public List<SourceFile> Details { get; set; }
        }

        public Result Results { get; private set; }

        public LinesCounter(ISourceLineAnalyzer sourceLineAnalyzer)
        {
            this.sourceLineAnalyzer = sourceLineAnalyzer;
            Results = new Result();
            Results.Details = new List<SourceFile>();
        }

        public void Count(List<SourceFile> sourceFiles)
        {
            Result.OverallResult r = Results.Overall;

            foreach (SourceFile f in sourceFiles)
            {
                f.GetAnalyzed(sourceLineAnalyzer);
                r.TotalLines += f.LinesOfCode;
                r.SourceLines += f.SourceLinesOfCode;
                r.EffectiveLines += f.EffectiveLinesOfCode;
                r.CommentLines += f.CommentLines;

                Results.Details.Add(f);
            }
            Results.Overall = r;
        }
    }
}

