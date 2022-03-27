using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitTests
{
    [TestClass]
    public class EOFTests
    {
        [TestMethod]
        public void EofAtGateInputs()
        {
            CircuitTester.Execute("EofAtGateInputs");
        }

        [TestMethod]
        public void EofAtGateOutputs()
        {
            CircuitTester.Execute("EofAtGateOutputs");
        }

        [TestMethod]
        public void EofAtGateTransitionFunction()
        {
            CircuitTester.Execute("EofAtGateTransitionFunction");
        }


        [TestMethod]
        public void EofAtGateEnd()
        {
            CircuitTester.Execute("EofAtGateEnd");
        }


        [TestMethod]
        public void EofAtNetwork()
        {
            CircuitTester.Execute("EofAtNetwork");
        }

        [TestMethod]
        public void EofAtNetworkInputs()
        {
            CircuitTester.Execute("EofAtNetworkInputs");
        }

        [TestMethod]
        public void EofAtNetworkOutputs()
        {
            CircuitTester.Execute("EofAtNetworkOutputs");
        }

        [TestMethod]
        public void EofAtNetworkGates()
        {
            CircuitTester.Execute("EofAtNetworkGates");
        }

        [TestMethod]
        public void EofAtNetworkBindings()
        {
            CircuitTester.Execute("EofAtNetworkBindings");
        }

        [TestMethod]
        public void EofAtNetworkEnd()
        {
            CircuitTester.Execute("EofAtNetworkEnd");
        }
    }
}
