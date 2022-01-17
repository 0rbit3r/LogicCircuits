using System;
using System.Collections.Generic;
using System.Text;

namespace LogicCircuits
{
    public enum CirDefExceptionType
    {
        Duplicate,
        MissingKeyword,
        BindingRule,
        SyntaxError
    }
    public class CircuitDefinitionException : Exception
    {
        public CircuitDefinitionException(int line, CirDefExceptionType type)
            : base(CreateMessage(line, type))
        { }

        private static string CreateMessage(int line, CirDefExceptionType type)
        {
            string typeStr;
            switch (type)
            {
                case CirDefExceptionType.Duplicate:
                    typeStr = "Duplicate";
                    break;
                case CirDefExceptionType.MissingKeyword:
                    typeStr = "Missing keyword";
                    break;
                case CirDefExceptionType.BindingRule:
                    typeStr = "Binding rule";
                    break;
                case CirDefExceptionType.SyntaxError:
                    typeStr = "Syntax error";
                    break;
                default:
                    typeStr = "Unknown Error";
                    break;

            }

            return $"Line {line}: {typeStr}.";
        }
    }
}
