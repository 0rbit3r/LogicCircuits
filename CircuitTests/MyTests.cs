using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitTests
{
    [TestClass]
    public class MyTests
    {
        [TestMethod]
        public void TwoLayerSimple()
        {
            CircuitTester.TestFromPath("data/TwoLayerSimple");
        }
    }
}
