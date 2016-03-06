using System;

namespace LinesCount
{
    public class CSharpSourceLineAnalyzer : ISourceLineAnalyzer
    {
        public bool IsSourceLine(string sourceLine)
        {
            return false;
        }
        public bool IsCommentLine(string sourceLine)
        {
            throw new NotImplementedException();
        }
        public bool IsEffectiveCodeLine(string sourceLine)
        {
            throw new NotImplementedException();
        }
    }
}

