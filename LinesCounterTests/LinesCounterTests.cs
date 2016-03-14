using LinesCount;
using NUnit.Framework;
using System;

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
    }
}

