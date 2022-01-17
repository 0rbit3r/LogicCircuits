using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogicCircuits
{
    public class CircuitDefinitionReader
    {
        private StreamReader reader;
        private string? nextLine;
        public int LineNumber { get; private set; } = -1;
        
        public CircuitDefinitionReader(string file)
        {
            reader = new StreamReader(file);
            nextLine = GetNextNonEmptyLine();
        }

        public string[] ReadLine()
        {
            var tempLine = nextLine;
            if(tempLine == null)
            {
                return new string[0];
            }
            nextLine = GetNextNonEmptyLine();
            return tempLine.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] PeekLine()
        {
            return nextLine.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
        }

        private string? GetNextNonEmptyLine()
        {
            string? line;
            do
            {
                LineNumber++;
                line = reader.ReadLine();
            } while (line != null && !line.StartsWith(";") && line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries).Length == 0);

            return line;
        }
    }
}
