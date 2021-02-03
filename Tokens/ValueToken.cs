using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    public class ValueToken : AbstractToken
    {
        private readonly string inputString;

        public object Value { get; private set; }

        public ValueToken(object value, string inputString)
        {
            this.inputString = inputString;
            Value = value;
        }

        public override string GetInputString()
        {
            return inputString;
        }

        public bool IsNumeric()
        {
            return Value is int 
                || Value is float 
                || Value is byte 
                || Value is double 
                || Value is decimal
                || Value is short
                || Value is uint
                || Value is ushort
                || Value is Int64;
        }

        public bool IsFloat()
        {
            return Value is float
                || Value is double
                || Value is decimal;
        }

        public bool IsBoolean()
        {
            return Value is bool
                || Value is Boolean;
        }
    }
}
