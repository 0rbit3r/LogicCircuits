using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitTests
{
    [TestClass]
    public class WrongNamesTests
    {
        [TestMethod]
        public void GateNameContainsSpace()
        {
            CircuitTester.Execute("GateNameContainsSpace");
        }

        [TestMethod]
        public void GateNameContainsSemicolon()
        {
            CircuitTester.Execute("GateNameContainsSemicolon");
        }

        [TestMethod]
        public void GateNameContainsDot()
        {
            CircuitTester.Execute("GateNameContainsDot");
        }

        [TestMethod]
        public void GateNameContainsArrow()
        {
            CircuitTester.Execute("GateNameContainsArrow");
        }

        [TestMethod]
        public void GateStartsWithEnd()
        {
            CircuitTester.Execute("GateStartsWithEnd");
        }

        [TestMethod]
        public void InstanceNameContainsSpace()
        {
            CircuitTester.Execute("InstanceNameContainsSpace");
        }

        [TestMethod]
        public void InstanceNameContainsSemicolon()
        {
            CircuitTester.Execute("InstanceNameContainsSemicolon");
        }

        [TestMethod]
        public void InstanceNameContainsDot()
        {
            CircuitTester.Execute("InstanceNameContainsDot");
        }

        [TestMethod]
        public void InstanceNameContainsArrow()
        {
            CircuitTester.Execute("InstanceNameContainsArrow");
        }

        [TestMethod]
        public void InstanceNameStartsWithEnd()
        {
            CircuitTester.Execute("InstanceNameStartsWithEnd");
        }

        [TestMethod]
        public void GateInputNameContainsSemicolon()
        {
            CircuitTester.Execute("GateInputNameContainsSemicolon");
        }

        [TestMethod]
        public void GateInputNameContainsDot()
        {
            CircuitTester.Execute("GateInputNameContainsDot");
        }

        [TestMethod]
        public void GateInputNameContainsArrow()
        {
            CircuitTester.Execute("GateInputNameContainsArrow");
        }

        [TestMethod]
        public void CircuitOutputNameStartsWithEnd()
        {
            CircuitTester.Execute("CircuitOutputNameStartsWithEnd");
        }

        [TestMethod]
        public void CircuitOutputNameContainsSemicolon()
        {
            CircuitTester.Execute("CircuitOutputNameContainsSemicolon");
        }

        [TestMethod]
        public void CircuitOutputNameContainsDot()
        {
            CircuitTester.Execute("CircuitOutputNameContainsDot");
        }

        [TestMethod]
        public void CircuitOutputNameContainsArrow()
        {
            CircuitTester.Execute("CircuitOutputNameContainsArrow");
        }
    }
}
