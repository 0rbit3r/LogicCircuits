using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicCircuits
{
    public class LogicGate
    {
        public readonly Dictionary<string, int> InputDict;
        public readonly Dictionary<string, int> OutputDict;

        public Value[][] DefTable;

        public LogicGate(CircuitDefinitionReader reader)
        {
            string[] line = reader.ReadLine();

            if (line[0] == "inputs")
            {
                InputDict = new Dictionary<string, int>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    InputDict.Add(line[i], i - 1);
                    CheckIdentifierSyntax(line[i], reader.LineNumber);
                }
            }
            else
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            line = reader.ReadLine();
            if (line[0] == "outputs")
            {
                OutputDict = new Dictionary<string, int>(line.Length - 1);
                for (int i = 1; i < line.Length; i++)
                {
                    OutputDict.Add(line[i], i - 1);
                    CheckIdentifierSyntax(line[i], reader.LineNumber);
                }
            }
            else
            {
                throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.MissingKeyword);
            }

            var tempDefTable = new List<Value[]>();
            while ((line = reader.ReadLine())[0] != "end")
            {
                if (line.Length != InputDict.Count + OutputDict.Count)
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.SyntaxError);
                }

                var valuesArr = Array.ConvertAll(line, x =>
                {
                    switch (x)
                    {
                        case "0": return Value.Zero;
                        case "1": return Value.One;
                        case "?": return Value.Undefined;
                        default: throw new Exception("Invalid state");
                    }
                });

                //Duplicate definition
                if (tempDefTable.Any(arr => {
                    var areSame = true;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (valuesArr[i] != arr[i])
                        {
                            areSame = false;
                            break;
                        }
                    }
                    return areSame;
                }))
                {
                    throw new CircuitDefinitionException(reader.LineNumber, CirDefExceptionType.Duplicate);
                }

                tempDefTable.Add(valuesArr);
            }

            DefTable = tempDefTable.ToArray();
        }

        public Value[] GetOutputFor(Value[] input)
        {
            List<Value> outList = new List<Value>();
            for (int row = 0; row < DefTable.Length; row++)
            {
                bool found = true;
                for (int column = 0; column < input.Length; column++)
                {
                    if(DefTable[row][column] != input[column])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    for (int i = input.Length; i < DefTable[0].Length; i++)
                    {
                        outList.Add(DefTable[row][i]);
                    }

                    return outList.ToArray();
                }
            }

            //Input is not in defTable
            if(Array.IndexOf(input, Value.Undefined) != -1)
            {
                for (int i = 0; i < OutputDict.Count; i++)
                {
                    outList.Add(Value.Undefined);
                }
            }
            else
            {
                for (int i = 0; i < OutputDict.Count; i++)
                {
                    outList.Add(Value.Zero);
                }
            }

            return outList.ToArray();
        }

        private void CheckIdentifierSyntax(string line, int lineNum)
        {
            if (line.Contains('.') ||
                    line.Contains(';') || line.Contains("->") || line.StartsWith("end"))
            {
                throw new CircuitDefinitionException(lineNum, CirDefExceptionType.SyntaxError);
            }
        }
    }
}
