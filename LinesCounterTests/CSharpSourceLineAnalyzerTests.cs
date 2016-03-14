using NUnit.Framework;
using System;
using LinesCount;

namespace LinesCountTests
{
    [TestFixture()]
    public class CSharpSourceLineAnalyzerTests
    {
        private ISourceLineAnalyzer sla;

        [SetUp]
        public void SetUp()
        {
            sla = new CSharpSourceLineAnalyzer();
        }

        [Test()]
        public void TestIsolatedEmptyLine()
        {
            const string sourceLine = "";
            Assert.IsFalse(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestIsolatedSourceLine()
        {
            const string sourceLine = "public void Foo()";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestIsolatedOpeningBrace()
        {
            const string sourceLine = "{";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestIsolatedClosingBrace()
        {
            const string sourceLine = "}";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestLineCommentLine()
        {
            const string sourceLine = "// some comment";
            Assert.IsFalse(sla.IsSourceLine(sourceLine));
            Assert.IsTrue(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestSingleBlockCommentLine()
        {
            const string sourceLine = "/* some block comment */";
            Assert.IsFalse(sla.IsSourceLine(sourceLine));
            Assert.IsTrue(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestBlockCommentOnlyAtStartOfLine()
        {
            const string sourceLine = "/* comment */ if (x == 17)";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestBlockCommentOnlyAtEndOfLine()
        {
            const string sourceLine = "if (x == 42) /* well */";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestBlockCommentInTheMiddleOfSourceLine()
        {
            const string sourceLine = "if /* maybe switch */ (x == 42)";
            Assert.IsTrue(sla.IsSourceLine(sourceLine));
            Assert.IsFalse(sla.IsCommentLine(sourceLine));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestBlockCommentNotEndedInTheSameLine()
        {
            const string sourceLine = "/* This is an n-line comment (n > 1)";
            Assert.IsFalse(sla.IsSourceLine(sourceLine));
            Assert.IsTrue(sla.IsCommentLine(sourceLine));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine));
        }

        [Test]
        public void TestFurtherLinesOfMultilineBlockComment()
        {
            const string sourceLine1 = "/* This is an n-line comment (n > 1)";
            const string sourceLine2 = "the second line of this comment";
            const string sourceLine3 = "the third line is the last one */";
            const string sourceLine4 = "x = 42;";

            Assert.IsFalse(sla.IsSourceLine(sourceLine1));
            Assert.IsFalse(sla.IsSourceLine(sourceLine2));
            Assert.IsFalse(sla.IsSourceLine(sourceLine3));
            Assert.IsTrue(sla.IsSourceLine(sourceLine4));

            Assert.IsTrue(sla.IsCommentLine(sourceLine1));
            Assert.IsTrue(sla.IsCommentLine(sourceLine2));
            Assert.IsTrue(sla.IsCommentLine(sourceLine3));
            Assert.IsFalse(sla.IsCommentLine(sourceLine4));

            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine1));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine2));
            Assert.IsFalse(sla.IsEffectiveCodeLine(sourceLine3));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine4));
        }

        [Test]
        public void TestMultiLineCommentEndingInTheMiddleOfALine()
        {
            const string sourceLine1 = "/* This is an n-line comment (n > 1)";
            const string sourceLine2 = "the second line of this comment";
            const string sourceLine3 = "the third line is the last one */ x = 17;";

            Assert.IsTrue(sla.IsSourceLine(sourceLine3));
            Assert.IsFalse(sla.IsCommentLine(sourceLine3));
            Assert.IsTrue(sla.IsEffectiveCodeLine(sourceLine3));
        }

        [Test]
        public void TestWhetherWhitspacesAtBeginningAndEndAreIgnored()
        {
            const string sourceLine1 = "          ";
            Assert.IsFalse(sla.IsSourceLine(sourceLine1));
        }
    }
}

