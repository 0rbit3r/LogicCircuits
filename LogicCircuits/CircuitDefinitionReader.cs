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
        private int nextLineNumber { get; set; } = 0;
        public int LineNumber { get; private set; }
        
        public CircuitDefinitionReader(string file)
        {
            try
            {
                reader = new StreamReader(file);
            }
            catch(Exception e)
            {
                throw new CircuitDefinitionException(0, CirDefExceptionType.FileError);
            }
            nextLine = GetNextNonEmptyLine();
        }

        public string[] ReadLine()
        {
            LineNumber = nextLineNumber;
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
                line = reader.ReadLine();
                nextLineNumber++;
                if (line == null)
                    break;

            } while (line.StartsWith(";") || line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries).Length == 0);

            return line;
        }
    }
}
