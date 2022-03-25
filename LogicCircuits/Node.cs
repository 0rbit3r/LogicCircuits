using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public enum Value
    {
        Zero,
        One,
        Undefined
    }
    public class Node
    {
        //Only for debug purposes
        public List<string> UsedBy = new List<string>();

        public Value Value { get; private set; }
        public Value FutureVal { get; set; }

        public void Update()
        {
            Value = FutureVal;
        }

        public Node()
        {
            Value = Value.Undefined;
            FutureVal = Value.Undefined;
        }

        public Node(Value val)
        {
            Value = val;
        }
    }
}
