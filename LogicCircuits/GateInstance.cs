﻿using System;
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
    
        public void ComputeNextState()
        {
            Value[] inputValues = Array.ConvertAll(Inputs, i => i.Value);

            FutureOutputsValues = Gate.GetOutputFor(inputValues);

            LastInputValues = Array.ConvertAll(Inputs, i => i.Value);
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

            return isStable;
        }
    }
}
