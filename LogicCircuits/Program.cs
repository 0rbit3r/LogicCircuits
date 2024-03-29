﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Argument error.");
                return;
            }
            try
            {
                LogicCircuit circuit = CircuitBuilder.FromFile(args[0]);
                circuit.HandleConsoleInputStream();
            }
            catch (CircuitDefinitionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
