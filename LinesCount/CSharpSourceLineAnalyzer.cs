using System;

namespace LinesCount
{
    public class CSharpSourceLineAnalyzer : ISourceLineAnalyzer
    {
        private bool isInBlockComment = false;

        /// <summary>
        /// Determines whether sourceLine contains a source line.
        /// </summary>
        /// <description>A source line is a line in source code not beeing a comment line and not being an empty line</description>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="sourceLine">Source line.</param>
        public bool IsSourceLine(string sourceLine)
        {
            return !IsEmptyLine(sourceLine.Trim()) && !IsCommentLine(sourceLine.Trim());
        }

        static bool IsEmptyLine(string sourceLine)
        {
            return sourceLine.Length == 0;
        }

        /// <summary>
        /// Determines whether sourceLine contains a comment line.
        /// </summary>
        /// <description>A comment line is a line in source code which has no other content than comment.</description>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="sourceLine">Source line.</param>
        public bool IsCommentLine(string sourceLine)
        {
            sourceLine = sourceLine.Trim();
            bool isCommentLine;
            if (isInBlockComment)
            {
                isInBlockComment = !BlockCommentEndsInLine(sourceLine);
                isCommentLine = !BlockCommentEndsInLine(sourceLine) || sourceLine.EndsWith("*/");
            }
            else
            {
                isCommentLine = BlockCommentStartsOrIsCommentLine(sourceLine);
            }
            return isCommentLine;
        }

        private bool BlockCommentStartsOrIsCommentLine(string sourceLine)
        {
            isInBlockComment = BlockCommentStartsInLine(sourceLine);
            bool isCommentLine = IsLineCommentLine(sourceLine) || IsBlockCommentOverOneLine(sourceLine) || isInBlockComment;
            return isCommentLine;
        }

        private bool BlockCommentEndsInLine(string sourceLine)
        {
            return sourceLine.Contains("*/");
        }

        private bool IsLineCommentLine(string sourceLine)
        {
            return sourceLine.StartsWith("//");
        }

        static bool IsBlockCommentOverOneLine(string sourceLine)
        {
            return sourceLine.StartsWith("/*") && sourceLine.EndsWith("*/");
        }

        static bool BlockCommentStartsInLine(string sourceLine)
        {
            return sourceLine.StartsWith("/*") && !sourceLine.Contains("*/");
        }

        /// <summary>
        /// Determines whether sourceLine contains an effective code line.
        /// </summary>
        /// <description>An effective code line is a line in source code which is a source line and has "some effect". In
        /// particular it does not count single block start or block end statements like "{" and "}" in C/Java/etc.</description>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="sourceLine">Source line.</param>
        public bool IsEffectiveCodeLine(string sourceLine)
        {
            sourceLine = sourceLine.Trim();
            return IsSourceLine(sourceLine) &&
            sourceLine != "{" && sourceLine != "}";
        }
    }
}

