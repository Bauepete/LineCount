using System;

namespace LinesCount
{
    public interface ISourceLineAnalyzer
    {
        /// <summary>
        /// Determines whether sourceLine contains a source line.
        /// </summary>
        /// <description>
        /// A source line is a line in source code not beeing a comment line and not being an empty line
        /// </description>
        /// <returns><c>true</c> if sourceLine contains a source line; otherwise, <c>false</c>.</returns>
        /// <param name="sourceLine">Source line.</param>
        bool IsSourceLine(string sourceLine);

        /// <summary>
        /// Determines whether sourceLine contains a comment line.
        /// </summary>
        /// <description>
        /// A comment line is a line in source code which has no other content than comment.
        /// </description>
        /// <returns><c>true</c> if sourceLine contains a comment line; otherwise, <c>false</c>.</returns>
        /// <param name="sourceLine">Source line.</param>
        bool IsCommentLine(string sourceLine);

        /// <summary>
        /// Determines whether sourceLine contains an effective code line.
        /// </summary>
        /// <description>
        /// An effective code line is a line in source code which is a source line and has "some effect". In
        /// particular it does not count single block start or block end statements like "{" and "}" in C/Java/etc.
        /// </description>
        /// <returns><c>true</c> if sourceLine contains an effective code line; otherwise, <c>false</c>.</returns>
        /// <param name="sourceLine">Source line.</param>
        bool IsEffectiveCodeLine(string sourceLine);
    }
}

