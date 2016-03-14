using System;
using System.Collections.Generic;

namespace LinesCount
{
    public class LinesCounter
    {
        public class Result {
            public struct OverallResult {
                public int TotalLines;
                public int SourceLines;
                public int EffectiveLines;
                public int CommentLines;
            }
            public Result.OverallResult Overall { get; set; }
            public List<SourceFile> Details { get; set; }
        }

        public Result Results { get; private set;}

        public LinesCounter(ISourceLineAnalyzer sourceLineAnalyzer)
        {
        }

        public void Count(List<SourceFile> sourceFiles)
        {

        }


    }
}

