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
        static readonly string TestCasesPath = "../../../../TestCases/";
        public static void Execute(string name)
        {
            var standardOutput = Console.Out;

            TextReader input = null;
            try
            {
                input = new StreamReader(TestCasesPath + name + "/std.in");
                Console.SetIn(input);
            }
            catch (Exception e)
            {

            }
            StreamWriter output = new StreamWriter(TestCasesPath + name + "/TestOutput.out");
            Console.SetOut(output);

            Program.Main(new string[] { TestCasesPath + name + "/hradla.in" });

            output.Close();

            Console.SetOut(standardOutput);

            StreamReader correctOutput = new StreamReader(TestCasesPath + name + "/std.out");
            StreamReader realOutput = new StreamReader(TestCasesPath + name + "/TestOutput.out");

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
