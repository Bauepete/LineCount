using LinesCount;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LinesCountTests
{
    public class LinesCounterTests
    {
        private LinesCounter lc;
        private SourceFile f1;
        private SourceFile f2;

        [SetUp]
        public void SetUp()
        {
            lc  = new LinesCounter(new CSharpSourceLineAnalyzer());
            f1 = new SourceFile("f1.cs", new string[]{ "// f1.cs", "class F1", "{", "}" });
            f2 = new SourceFile("f1.cs", new string[]{ "// f1.cs", "class F1", "{", "F1()", "{", "}", "}" });
        }

        [Test]
        public void TestConstruction()
        {
            Assert.AreEqual(0, lc.Results.Overall.TotalLines);
            Assert.AreEqual(0, lc.Results.Overall.SourceLines);
            Assert.AreEqual(0, lc.Results.Overall.EffectiveLines);
            Assert.AreEqual(0, lc.Results.Overall.CommentLines);
            Assert.AreEqual(0, lc.Results.Details.Count);
        }

        [Test]
        public void TestFirstFileOveralls()
        {
            lc.Count(new List<SourceFile>(new SourceFile[]{f1}));
            Assert.AreEqual(4, lc.Results.Overall.TotalLines);
            Assert.AreEqual(3, lc.Results.Overall.SourceLines);
            Assert.AreEqual(1, lc.Results.Overall.EffectiveLines);
            Assert.AreEqual(1, lc.Results.Overall.CommentLines);
        }

        [Test]
        public void TestFirstFileDetails()
        {
            lc.Count(new List<SourceFile>(new SourceFile[]{ f1 }));
            Assert.AreEqual(f1, lc.Results.Details[0]);
        }

        [Test]
        [Ignore]
        public void TestTwoFiles()
        {
            lc.Count(new List<SourceFile>(new SourceFile[]{ f1, f2 })); 

        }
    }
}

