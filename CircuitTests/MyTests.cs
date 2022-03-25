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
            CircuitTester.Execute("TwoLayerSimple");
        }

        [TestMethod]
        public void UnconnectedOutput()
        {
            CircuitTester.Execute("UnconnectedOutput");
        }

        [TestMethod]
        public void UnconnectedInput()
        {
            CircuitTester.Execute("UnconnectedInput");
        }

        [TestMethod]
        public void InputToInput()
        {
            CircuitTester.Execute("InputToInput");
        }
    }
}
