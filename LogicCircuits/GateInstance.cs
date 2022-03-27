using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicCircuits
{
    public class GateInstance
    {
        private readonly LogicGate Gate;

        public string Name;

        Node[] Inputs;
        Node[] Outputs;

        Value[] FutureOutputsValues;
        Value[] LastInputValues;

        public GateInstance(string name, LogicGate gate)
        {
            Name = name;
            Gate = gate;
            Inputs = new Node[Gate.InputDict.Count];
            Outputs = new Node[Gate.OutputDict.Count];
            LastInputValues = new Value[Gate.InputDict.Count];

            int i = 0;
            foreach (var keyValPair in Gate.OutputDict)
            {
                var node = new Node();
                if (gate.InputDict.Count == 0)
                {
                    node = new Node(gate.DefTable[0][i]);
                }
                node.UsedBy.Add(name + "." + keyValPair.Key);
                Outputs[i++] = node;
            }
        }
        public void InstantiateNullInputs()
        {
            for (int i = 0; i < Inputs.Length; i++)
            {
                if (Inputs[i] == null)
                {
                    Inputs[i] = new Node(Value.Undefined);
                }
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

        public CirDefExceptionType ConnectNodeToInput(Node node, string inputName)
        {
            if (!Gate.InputDict.ContainsKey(inputName))
            {
                return CirDefExceptionType.BindingRule;
            }
            if(node.UsedBy.Contains(Name + "." + inputName))
            {
                return CirDefExceptionType.Duplicate;
            }
            if(Inputs[Gate.InputDict[inputName]] != null)
            {
                return CirDefExceptionType.BindingRule;
            }

            Inputs[Gate.InputDict[inputName]] = node;

            node.UsedBy.Add(Name + "." + inputName);

            return CirDefExceptionType.Success;
        }

        public void ComputeNextState()
        {
            LastInputValues = Array.ConvertAll(Inputs, i => i.Value);

            FutureOutputsValues = Gate.GetOutputFor(LastInputValues);
        }

        public void UpdateState()
        {
            for (int i = 0; i < Outputs.Length; i++)
            {
                //isStable &= Outputs[i].Value == FutureOutputsValues[i];
                Outputs[i].Value = FutureOutputsValues[i];
            }
        }

        public bool IsStable()
        {
            var isStable = true;
            for (int i = 0; i < Inputs.Length; i++)
            {
                isStable &= Inputs[i].Value == LastInputValues[i];
            }

            Value[] inputValues = Array.ConvertAll(Inputs, i => i.Value);

            var computedOutputs = Gate.GetOutputFor(inputValues);

            for (int i = 0; i < Outputs.Length; i++)
            {
                isStable &= Outputs[i].Value == computedOutputs[i];
            }

            return isStable;
        }

        public override string ToString()
        {
            return $"{Name} {Outputs.First().Value} {IsStable()}";
        }
    }
}
