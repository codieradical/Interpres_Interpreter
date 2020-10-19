using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.lexer
{
    class ValueToken : AbstractToken
    {
        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            private set
            {
                _value = value;
            }
        }


        public ValueToken(object value)
        {
            Value = value;
        }

        public static ValueToken FromString(string valueString)
        {
            if (valueString.StartsWith("\"") && valueString.EndsWith("\""))
                return new ValueToken(valueString.Substring(1, valueString.Length - 2));

            if (valueString.StartsWith("\'") && valueString.EndsWith("\'") && valueString.Length == 3)
                return new ValueToken(valueString.ElementAt(1));

            if (valueString.ToLower().EndsWith("f"))
            {
                if (float.TryParse(valueString, out float floatValue))
                    return new ValueToken(floatValue);
                else
                    throw new Exception("Invalid float value " + valueString);
            }

            if (valueString.Contains("."))
            {
                if (decimal.TryParse(valueString, out decimal decimalValue))
                    return new ValueToken(decimalValue);
                else
                    throw new Exception("Invalid decimal value " + valueString);
            }

            if (bool.TryParse(valueString, out bool boolValue))
                return new ValueToken(boolValue);

            if (long.TryParse(valueString, out long longValue))
                return new ValueToken(longValue);

            throw new Exception("Invalid value " + valueString);
        }

        public override string ToString()
        {
            return Value.ToString();
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
