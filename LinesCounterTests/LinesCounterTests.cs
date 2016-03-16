using LinesCount;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LinesCountTests
{
    public class LinesCounterTests
    {
        [Test]
        public void TestConstruction()
        {
            LinesCounter lc = new LinesCounter(new CSharpSourceLineAnalyzer());
            Assert.AreEqual(0, lc.Results.Overall.TotalLines);
            Assert.AreEqual(0, lc.Results.Overall.SourceLines);
            Assert.AreEqual(0, lc.Results.Overall.EffectiveLines);
            Assert.AreEqual(0, lc.Results.Overall.CommentLines);
            Assert.AreEqual(0, lc.Results.Details.Count);
        }

        [Test]
        public void TestFirstFile()
        {
            LinesCounter lc = new LinesCounter(new CSharpSourceLineAnalyzer());
            SourceFile f1 = new SourceFile("f1.cs", new string[]{ "// f1.cs", "class F1", "{", "}" });

            lc.Count(new List<SourceFile>(new SourceFile[]{f1}));
            Assert.AreEqual(4, lc.Results.Overall.TotalLines);
            Assert.AreEqual(3, lc.Results.Overall.SourceLines);
            Assert.AreEqual(1, lc.Results.Overall.EffectiveLines);
            Assert.AreEqual(1, lc.Results.Overall.CommentLines);
        }
    }
}

