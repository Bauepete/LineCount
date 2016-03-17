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

            sourceFiles[0].GetAnalyzed(sourceLineAnalyzer);
            r.TotalLines = sourceFiles[0].LinesOfCode;
            r.SourceLines = sourceFiles[0].SourceLinesOfCode;
            r.EffectiveLines = sourceFiles[0].EffectiveLinesOfCode;
            r.CommentLines = sourceFiles[0].CommentLines;

            Results.Details.Add(sourceFiles[0]);

            Results.Overall = r;
        }


    }
}

