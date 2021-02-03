using System;

namespace Interpreter.Syntax
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message) {}
    }
}