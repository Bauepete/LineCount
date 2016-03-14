using LinesCount;
using NUnit.Framework;
using System;

namespace LinesCountTests
{
    public class LinesCounterTests
    {

        public void TestConstruction()
        {
            LinesCounter lc = new LinesCounter(new CSharpSourceLineAnalyzer());
            Assert.AreEqual(0, lc.Results.Details.Count);
        }
    }
}

