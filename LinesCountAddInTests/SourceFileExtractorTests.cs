using LinesCountAddIn;
using NUnit.Framework;
using System;

namespace LinesCountAddInTests
{
    [TestFixture()]
    public class SourceFileExtractorTests
    {
        [Test()]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestConstructionWithNull()
        {
            SourceFileExtractor sfa = new SourceFileExtractor(null);
            sfa.ToString(); // dummy to avoid warning since #pragma disable 414 does not have any effect
        }
    }
}

