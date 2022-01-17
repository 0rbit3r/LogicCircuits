using LogicCircuits;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CircuitTests
{
    public static class CircuitTester
    {
        public static void TestFromPath(string path)
        {
            var standardOutput = Console.Out;

            TextReader input = null;
            try
            {
                input = new StreamReader(path + "/std.in");
                Console.SetIn(input);
            }
            catch (Exception e)
            {

            }
            StreamWriter output = new StreamWriter(path + "/TestOutput.out");
            Console.SetOut(output);

            LogicCircuit.Main(new string[] { path + "/hradla.in" });

            output.Close();

            Console.SetOut(standardOutput);

            StreamReader correctOutput = new StreamReader(path + "/std.out");
            StreamReader realOutput = new StreamReader(path + "/TestOutput.out");

            bool testPassed = true;

            string correctLine;
            int line = 0;
            while ((correctLine = correctOutput.ReadLine()) != null)
            {
                line++;
                string realLine = realOutput.ReadLine();
                if(realLine != correctLine)
                {
                    testPassed = false;
                    Console.WriteLine($"Error at line {line}:");
                    Console.WriteLine($"      Expected: {correctLine}");
                    Console.WriteLine($"      But got:  {realLine}");
                }
            }


            Assert.IsTrue(testPassed);
        }
    }
}
