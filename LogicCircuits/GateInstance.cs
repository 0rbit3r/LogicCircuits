using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public class GateInstance
    {
        private readonly LogicGate Gate;

        public string Name;

        Node[] Inputs;
        Node[] Outputs;

        public GateInstance(string name, LogicGate gate)
        {
            Name = name;
            Gate = gate;
            Inputs = new Node[Gate.InputDict.Count];
            Outputs = new Node[Gate.OutputDict.Count];

            int i = 0;
            foreach (var keyValPair in Gate.OutputDict)
            {
                var node = new Node();
                node.UsedBy.Add(name + "." + keyValPair.Key);
                Outputs[i++] = node;
            }
        }

        public Node GetOutput(string name)
        {
            if (!Gate.OutputDict.ContainsKey(name))
            {
                return null;
            }
            return Outputs[Gate.OutputDict[name]];
        }

        public bool ConnectNodeToInput(Node node, string inputName)
        {
            if(!Gate.InputDict.ContainsKey(inputName))
            {
                return false;
            }

            Inputs[Gate.InputDict[inputName]] = node;

            node.UsedBy.Add(Name + "." + inputName);

            return true;
        }
    
        public bool ComputeNextState()
        {
            bool isStable = true;

            Value[] values = Array.ConvertAll(Inputs, i => i.Value);

            Value[] computedOutputs = Gate.GetOutputFor(values);

            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i].FutureVal = computedOutputs[i];

                isStable &= Outputs[i].FutureVal == Outputs[i].Value;
            }

            return isStable;
        }

        public void UpdateState()
        {
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i].Update();
            }
        }
    }
}
