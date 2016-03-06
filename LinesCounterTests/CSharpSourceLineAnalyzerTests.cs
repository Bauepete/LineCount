using NUnit.Framework;
using System;
using LinesCount;

namespace LinesCounterTests
{
    [TestFixture()]
    public class CSharpSourceLineAnalyzerTests
    {
        [Test()]
        public void TestEmptyLine()
        {
            ISourceLineAnalyzer sla = new CSharpSourceLineAnalyzer();
            const string sl = "";
            Assert.IsFalse(sla.IsSourceLine(sl));
        }
    }
}

