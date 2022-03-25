using LogicCircuits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CircuitTests
{
    [TestClass]
    public class ProvidedTests
    {
        [TestMethod]
        public void Test01()
        {
            CircuitTester.Execute("01");
        }

        [TestMethod]
        public void Test04()
        {
            CircuitTester.Execute("04");
        }

        [TestMethod]
        public void Test05()
        {
            CircuitTester.Execute("05");
        }

        [TestMethod]
        public void Test07()
        {
            CircuitTester.Execute("07");
        }

        [TestMethod]
        public void Test08()
        {
            CircuitTester.Execute("08");
        }

        [TestMethod]
        public void Test09()
        {
            CircuitTester.Execute("09");
        }

        [TestMethod]
        public void Test16()
        {
            CircuitTester.Execute("16");
        }

        [TestMethod]
        public void Test17()
        {
            CircuitTester.Execute("17");
        }

        [TestMethod]
        public void Test20()
        {
            CircuitTester.Execute("20");
        }
    }
}
