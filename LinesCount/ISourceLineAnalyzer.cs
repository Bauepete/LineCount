using System;

namespace LinesCount
{
    public interface ISourceLineAnalyzer
    {
        bool IsSourceLine(string sourceLine);
        bool IsCommentLine(string sourceLine);
        bool IsEffectiveCodeLine(string sourceLine);
    }
}

