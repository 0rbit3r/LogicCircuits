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
            CircuitTester.TestFromPath("data/01");
        }

        [TestMethod]
        public void Test04()
        {
            CircuitTester.TestFromPath("data/04");
        }

        [TestMethod]
        public void Test05()
        {
            CircuitTester.TestFromPath("data/05");
        }

        [TestMethod]
        public void Test07()
        {
            CircuitTester.TestFromPath("data/07");
        }

        [TestMethod]
        public void Test08()
        {
            CircuitTester.TestFromPath("data/08");
        }

        [TestMethod]
        public void Test09()
        {
            CircuitTester.TestFromPath("data/09");
        }

        [TestMethod]
        public void Test16()
        {
            CircuitTester.TestFromPath("data/16");
        }

        [TestMethod]
        public void Test17()
        {
            CircuitTester.TestFromPath("data/17");
        }

        [TestMethod]
        public void Test20()
        {
            CircuitTester.TestFromPath("data/20");
        }
    }
}
