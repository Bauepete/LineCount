using Moq;
using NUnit.Framework;
using System;
using LinesCount;
using System.IO;

namespace LinesCounterTests
{
    public class SourceFileTests
    {
        private Mock<ISourceLineAnalyzer> sourceLineAnalyzerDouble;
        private string[] source1 =
            {
                "// How many lines are these?",
                "public void NumbersTillHundred()",
                "{",
                "",
                "for (int i = 1; i <= 100; i++)",
                "{",
                "Console.WriteLine(i)",
                "}",
                "}"
            };

        private string[] source2 =
            {
                "// How many lines are these?",
                "public void NumbersTillHundredAndTillThousand()",
                "{",
                "",
                "for (int i = 1; i <= 100; i++)",
                "{",
                "Console.WriteLine(i)",
                "}",
                "",
                "// a second time",
                "// should work too",
                "for (int i = 1; i <= 1000; i++)",
                "{",
                "Console.WriteLine(i)",
                "}",
                "}"
            };

        SourceFile srcFile1;
        SourceFile srcFile2;

        [SetUp]
        public void SetUp()
        {
            SetupSlaDouble();
            SetupSourceFiles();
        }

        private void SetupSlaDouble()
        {
            sourceLineAnalyzerDouble = new Mock<ISourceLineAnalyzer>();
            sourceLineAnalyzerDouble.Setup(sla => sla.IsSourceLine(It.Is<string>(str => str.Length != 0 && !str.StartsWith("//")))).Returns(true);
            sourceLineAnalyzerDouble.Setup(sla => sla.IsCommentLine(It.Is<string>(str => str.StartsWith("//")))).Returns(true);
            sourceLineAnalyzerDouble.Setup(sla => sla.IsEffectiveCodeLine(It.Is<string>(str => str.StartsWith("public") || str.StartsWith("for") || str.StartsWith("Console")))).Returns(true);
        }

        private void SetupSourceFiles()
        {
            srcFile1 = new SourceFile("foo.cs", source1, sourceLineAnalyzerDouble.Object);
            srcFile2 = new SourceFile("baa.cs", source2, sourceLineAnalyzerDouble.Object);
        }

        [Test]
        public void TestLOC()
        {
            Assert.AreEqual(9, srcFile1.LinesOfCode);
//            Assert.AreEqual(3, lc.EffectiveLinesOfCode);
        }

        [Test]
        public void TestLOC2()
        {
            Assert.AreEqual(16, srcFile2.LinesOfCode);
        }

        [Test]
        public void TestSLOC1()
        {
            Assert.AreEqual(7, srcFile1.SourceLinesOfCode);
        }

        [Test]
        public void TestSLOC2()
        {
            Assert.AreEqual(11, srcFile2.SourceLinesOfCode);
        }

        [Test]
        public void TestCLOC1()
        {
            Assert.AreEqual(1, srcFile1.CommentLines);
        }

        [Test]
        public void TestCLOC2()
        {
            Assert.AreEqual(3, srcFile2.CommentLines);
        }

        [Test]
        public void TestELOC1()
        {
            Assert.AreEqual(3, srcFile1.EffectiveLinesOfCode);
        }

        [Test]
        public void TestELOC2()
        {
            Assert.AreEqual(5, srcFile2.EffectiveLinesOfCode);
        }
    }
}

