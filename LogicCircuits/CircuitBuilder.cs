using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    static class CircuitBuilder
    {
        public static LogicCircuit FromFile(string path)
        {
            CircuitDefinitionReader reader = new CircuitDefinitionReader(path);
            LogicCircuit circuit = new LogicCircuit();
            circuit.Gates = new Dictionary<string, LogicGate>();
            circuit.GateInstances = new Dictionary<string, GateInstance>();
            circuit.CircuitInputs = new Dictionary<string, Node>();
            circuit.CircuitOutputs = new Dictionary<string, Node>();

            //Gate blueprints
            string[] line = new string[] { };
            while ((line = reader.ReadLine())[0] == "gate")
            {
                if (line.Length != 2) throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                CheckIdentifierSyntax(line[1], reader.LineNumber);
                circuit.Gates.Add(line[1], new LogicGate(reader));
            }
            if (line[0] != "network")
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            //Circuit Inputs
            line = reader.ReadLine();
            if (line[0] == "inputs")
            {
                circuit.CircuitInputs = new Dictionary<string, Node>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    CheckIdentifierSyntax(line[i], reader.LineNumber);
                    var node = new Node();
                    node.UsedBy.Add(line[i]);
                    circuit.CircuitInputs.Add(line[i], node);
                }
            }
            else
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            circuit.CircuitInputs.Add("1", new Node(Value.One));
            circuit.CircuitInputs.Add("0", new Node(Value.Zero));

            //Circuit Outputs
            line = reader.ReadLine();
            if (line[0] == "outputs")
            {
                circuit.CircuitOutputs = new Dictionary<string, Node>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    circuit.CircuitOutputs.Add(line[i], null);

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
                if (!circuit.Gates.ContainsKey(line[2]))
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                }

                var gate = circuit.Gates[line[2]];
                circuit.GateInstances.Add(line[1], new GateInstance(line[1], gate));
            }

            //Connections
            do
            {
                if (line.Length != 1)
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                }

                var inAndOut = line[0].Split("->");

                Connect(circuit, inAndOut[1], inAndOut[0], reader.LineNumber);

            } while ((line = reader.ReadLine())[0] != "end");

            AssertBindingRules(circuit, reader.LineNumber);

            return circuit;
        }

        private static void Connect(LogicCircuit circuit, string providerName, string receiverName, int line)
        {
            Node providerNode;

            //If the provider is Gates output
            if (providerName.Contains('.'))
            {
                var splitName = providerName.Split('.');
                if (!circuit.GateInstances.ContainsKey(splitName[0]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                providerNode = circuit.GateInstances[splitName[0]].GetOutput(splitName[1]);
            }
            //If the provider is network Input
            else
            {
                if (!circuit.CircuitInputs.ContainsKey(providerName))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                providerNode = circuit.CircuitInputs[providerName];
            }

            if (providerNode == null)
            {
                throw new CircuitDefinitionException(line, CirDefExceptionType.SyntaxError);
            }

            //If receiver is Gate input
            if (receiverName.Contains('.'))
            {
                var toNodeSplit = receiverName.Split('.');
                if (!circuit.GateInstances.ContainsKey(toNodeSplit[0]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
                if (!circuit.GateInstances[toNodeSplit[0]].ConnectNodeToInput(providerNode, toNodeSplit[1]))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
            }
            //If receiver is network output
            else
            {
                if (!circuit.CircuitOutputs.ContainsKey(receiverName))
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.SyntaxError);
                }
                circuit.CircuitOutputs[receiverName] = providerNode;
                providerNode.UsedBy.Add(receiverName);
            }
        }

        private static void AssertBindingRules(LogicCircuit circuit, int line)
        {
            foreach (var output in circuit.CircuitOutputs)
            {
                if (output.Value == null)
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
            }

            foreach (var input in circuit.CircuitInputs)
            {
                if (input.Key != "0" && input.Key != "1" && input.Value.UsedBy.Count == 1)
                {
                    throw new CircuitDefinitionException(line, CirDefExceptionType.BindingRule);
                }
            }
        }

        private static void CheckIdentifierSyntax(string line, int lineNum)
        {
            if (line.Contains('.') ||
                    line.Contains(';') || line.Contains("->") || line.StartsWith("end"))
            {
                throw new CircuitDefinitionException(lineNum, CirDefExceptionType.SyntaxError);
            }
        }

    }
}
