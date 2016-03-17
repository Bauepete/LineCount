using LinesCount;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LinesCountTests
{
    public class LinesCounterTests
    {
        private LinesCounter linesCounter;
        private SourceFile sourceFile0;
        private SourceFile sourceFile1;

        [SetUp]
        public void SetUp()
        {
            linesCounter  = new LinesCounter(new CSharpSourceLineAnalyzer());
            sourceFile0 = new SourceFile("f1.cs", new string[]{ "// f1.cs", "class F1", "{", "}" });
            sourceFile1 = new SourceFile("f1.cs", new string[]{ "// f1.cs", "class F1", "{", "F1()", "{", "}", "}" });
        }

        [Test]
        public void TestConstruction()
        {
            Assert.AreEqual(0, linesCounter.Results.Overall.TotalLines);
            Assert.AreEqual(0, linesCounter.Results.Overall.SourceLines);
            Assert.AreEqual(0, linesCounter.Results.Overall.EffectiveLines);
            Assert.AreEqual(0, linesCounter.Results.Overall.CommentLines);
            Assert.AreEqual(0, linesCounter.Results.Details.Count);
        }

        [Test]
        public void TestFirstFileOveralls()
        {
            linesCounter.Count(new List<SourceFile>(new SourceFile[]{sourceFile0}));
            Assert.AreEqual(4, linesCounter.Results.Overall.TotalLines);
            Assert.AreEqual(3, linesCounter.Results.Overall.SourceLines);
            Assert.AreEqual(1, linesCounter.Results.Overall.EffectiveLines);
            Assert.AreEqual(1, linesCounter.Results.Overall.CommentLines);
        }

        [Test]
        public void TestFirstFileDetails()
        {
            linesCounter.Count(new List<SourceFile>(new SourceFile[]{ sourceFile0 }));
            Assert.AreEqual(1, linesCounter.Results.Details.Count);
            Assert.AreEqual(sourceFile0, linesCounter.Results.Details[0]);
        }

        [Test]
        public void TestTwoFilesOveralls()
        {
            linesCounter.Count(new List<SourceFile>(new SourceFile[]{ sourceFile0, sourceFile1 })); 
            Assert.AreEqual(11, linesCounter.Results.Overall.TotalLines);
            Assert.AreEqual(9, linesCounter.Results.Overall.SourceLines);
            Assert.AreEqual(3, linesCounter.Results.Overall.EffectiveLines);
            Assert.AreEqual(2, linesCounter.Results.Overall.CommentLines);
        }

        [Test]
        public void TestTwoFilesDetails()
        {
            linesCounter.Count(new List<SourceFile>(new SourceFile[]{ sourceFile0, sourceFile1 }));
            Assert.AreEqual(2, linesCounter.Results.Details.Count);
            Assert.AreEqual(sourceFile0, linesCounter.Results.Details[0]);
            Assert.AreEqual(sourceFile1, linesCounter.Results.Details[1]);
        }
    }
}

