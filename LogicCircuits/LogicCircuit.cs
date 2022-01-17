using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public class LogicCircuit
    {

        Dictionary<string, LogicGate> Gates;
        Dictionary<string, Node> CircuitInputs;
        Dictionary<string, Node> CircuitOutputs;
        Dictionary<string, GateInstance> GateInstances;

        public static void Main(string[] args)
        {
            try
            {
                CircuitDefinitionReader reader = new CircuitDefinitionReader(args[0]);

                LogicCircuit circuit = new LogicCircuit(reader);
                circuit.HandleConsoleInputStream();
            }
            catch (CircuitDefinitionException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public LogicCircuit(CircuitDefinitionReader reader)
        {
            Gates = new Dictionary<string, LogicGate>();
            GateInstances = new Dictionary<string, GateInstance>();
            CircuitInputs = new Dictionary<string, Node>();
            CircuitOutputs = new Dictionary<string, Node>();

            //Gate blueprints
            string[] line = new string[] { };
            while ((line = reader.ReadLine())[0] == "gate")
            {
                if (line.Length != 2) throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                CheckIdentifierSyntax(line[1], reader.LineNumber);
                Gates.Add(line[1], new LogicGate(reader));
            }
            if (line[0] != "network")
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            //Circuit Inputs
            line = reader.ReadLine();
            if (line[0] == "inputs")
            {
                CircuitInputs = new Dictionary<string, Node>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    CheckIdentifierSyntax(line[i], reader.LineNumber);
                    var node = new Node();
                    node.UsedBy.Add(line[i]);
                    CircuitInputs.Add(line[i], node);
                }
            }
            else
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            CircuitInputs.Add("1", new Node(Value.One));
            CircuitInputs.Add("0", new Node(Value.Zero));

            //Circuit Outputs
            line = reader.ReadLine();
            if (line[0] == "outputs")
            {
                CircuitOutputs = new Dictionary<string, Node>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    CircuitOutputs.Add(line[i], null);

                    CheckIdentifierSyntax(line[i], reader.LineNumber);
                }
            }
            else
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            //Gate instances
            while ((line = reader.ReadLine())[0] == "gate")
            {
                if (!Gates.ContainsKey(line[2]))
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                }

                var gate = Gates[line[2]];
                GateInstances.Add(line[1], new GateInstance(line[1], gate));
            }

            //Connections
            do
            {
                if (line.Length != 1)
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                }

                var inAndOut = line[0].Split("->");

                Connect(inAndOut[1], inAndOut[0], reader.LineNumber);

            } while ((line = reader.ReadLine())[0] != "end");
        }

        private void CheckIdentifierSyntax(string line, int lineNum)
        {
            if (line.Contains('.') ||
                    line.Contains(';') || line.Contains("->") || line.StartsWith("end"))
            {
                throw new CircuitDefinitionException(lineNum, CirDefExceptionType.SyntaxError);
            }
        }

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
            while (!ComputeNextState() && ticks < 1000000 || ticks == 0)
            {
                ticks++;
                UpdateState();
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

        private void SetInput(Value[] inputVals)
        {
            int i = 0;
            foreach (var input in CircuitInputs)
            {
                if (i < CircuitInputs.Count - 2)
                {
                    input.Value.FutureVal = inputVals[i++];
                    input.Value.Update();
                }
            }
        }

        /// <summary>
        /// Updates futureVals of nodes
        /// </summary>
        /// <returns>True if the circuit is stable. False if at least one output node changed.</returns>
        private bool ComputeNextState()
        {
            bool isStable = true;
            foreach (var gateInstance in GateInstances)
            {
                isStable &= gateInstance.Value.ComputeNextState();
            }
            return isStable;
        }

        private void UpdateState()
        {
            foreach (var gateInstance in GateInstances)
            {
                gateInstance.Value.UpdateState();
            }
        }

        private void Connect(string providerName, string receiverName, int line)
        {
            Node providerNode;

            if (providerName.Contains('.'))
            {
                var splitName = providerName.Split('.');
                if (!GateInstances.ContainsKey(splitName[0]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                providerNode = GateInstances[splitName[0]].GetOutput(splitName[1]);
            }
            else
            {
                if (!CircuitInputs.ContainsKey(providerName))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                providerNode = CircuitInputs[providerName];
            }

            if(providerNode == null)
            {
                throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
            }

            if (receiverName.Contains('.'))
            {
                var toNodeSplit = receiverName.Split('.');
                if (!GateInstances.ContainsKey(toNodeSplit[0]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                if(!GateInstances[toNodeSplit[0]].ConnectNodeToInput(providerNode, toNodeSplit[1]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
            }
            else
            {
                if (!CircuitOutputs.ContainsKey(receiverName))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                CircuitOutputs[receiverName] = providerNode;
                providerNode.UsedBy.Add(receiverName);
            }
        }

        private bool CheckBindingRules()
        {
            return true;
        }
    }
}
