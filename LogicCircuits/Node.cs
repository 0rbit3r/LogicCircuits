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

        public List<string> UsedBy = new List<string>();

        public Value Value { get; set; }


        public Node()
        {
            Value = Value.Undefined;
        }

        public Node(Value val)
        {
            Value = val;
        }
    }
}
