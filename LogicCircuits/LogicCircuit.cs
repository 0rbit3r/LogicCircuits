using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public class LogicCircuit
    {

        public Dictionary<string, LogicGate> Gates;
        public Dictionary<string, Node> CircuitInputs;
        public Dictionary<string, Node> CircuitOutputs;
        public Dictionary<string, GateInstance> GateInstances;

        public void HandleConsoleInputStream()
        {
            string line;
            while ((line = Console.ReadLine()) != "end")
            {
                Console.WriteLine(HandleInput(line));
            }
        }

        public string HandleInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }
            var splitLine = input.Split();
            if (splitLine.Length != CircuitInputs.Count - 2)
            {
                return "Syntax error.";
            }
            Value[] vals = new Value[splitLine.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                switch (splitLine[i])
                {
                    case "0":
                        vals[i] = Value.Zero;
                        break;
                    case "1":
                        vals[i] = Value.One;
                        break;
                    case "?":
                        vals[i] = Value.Undefined;
                        break;
                    default:
                        return "Syntax error.";

                }
            }

            var ticks = 0;
            SetInput(vals);
            var isStable = false;
            while (!isStable && ticks < 1000000 || ticks == 0)
            {
                ticks++;

                ComputeNextState();
                UpdateState();
                isStable = IsStable();
            }

            var toReturn = new StringBuilder();

            toReturn.Append(ticks);
            foreach (var output in CircuitOutputs)
            {
                string digitVal = output.Value.Value == Value.One ? "1" : output.Value.Value == Value.Zero ? "0" : "?";
                toReturn.Append(" " + digitVal);
            }

            return toReturn.ToString();
        }
        public bool IsStable()
        {
            var isStable = true;
            foreach (var gate in GateInstances)
            {
                isStable &= gate.Value.IsStable();
            }

            return isStable;
        }

        private void SetInput(Value[] inputVals)
        {
            int i = 0;
            foreach (var input in CircuitInputs)
            {
                if (i < CircuitInputs.Count - 2)
                {
                    input.Value.Value = inputVals[i++];
                }
            }
        }

        /// <summary>
        /// Updates future values of nodes
        /// </summary>
        private void ComputeNextState()
        {
            foreach (var gateInstance in GateInstances)
            {
                gateInstance.Value.ComputeNextState();
            }
        }

        /// <summary>
        /// Sets Outputs of gate instances to its computed future values
        /// </summary>
        /// <returns></returns>
        private void UpdateState()
        {
            foreach (var gateInstance in GateInstances)
            {
                gateInstance.Value.UpdateState();
            }
        }

    }
}
