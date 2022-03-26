﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void GateInputToGateInput()
        {
            CircuitTester.Execute("GateInputToGateInput");
        }

        [TestMethod]
        public void GateInputToCircuitOutput()
        {
            CircuitTester.Execute("GateInputToCircuitOutput");
        }

        [TestMethod]
        public void GateOutputToGateOutput()
        {
            CircuitTester.Execute("GateOutputToGateOutput");
        }

        [TestMethod]
        public void GateOutputToCircuitInput()
        {
            CircuitTester.Execute("GateOutputToCircuitInput");
        }

        [TestMethod]
        public void Disconnected()
        {
            CircuitTester.Execute("Disconnected");
        }

        [TestMethod]
        public void Alternating()
        {
            CircuitTester.Execute("Alternating");
        }

        [TestMethod]
        public void FourAnds()
        {
            CircuitTester.Execute("FourAnds");
        }

        [TestMethod]
        public void WeirdFile()
        {
            CircuitTester.Execute("FourAnds");
        }

        [TestMethod]
        public void BindingRuleAtLine25()
        {
            CircuitTester.Execute("BindingRuleAtLine25");
        }
    }
}
