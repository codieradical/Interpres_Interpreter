using System;

namespace Interpreter.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNumeric(this object Value)
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

        public static bool IsFloat(this object Value)
        {
            return Value is float
                   || Value is double
                   || Value is decimal;
        }

        public static bool IsBoolean(this object Value)
        {
            return Value is bool
                   || Value is Boolean;
        }
        
        public static bool IsCharacter(this object Value)
        {
            return Value is char
                   || Value is Char;
        }
        
        public static bool IsString(this object Value)
        {
            return Value is string
                   || Value is String;
        }

        public static bool IsArray(this object Value)
        {
            return Value.GetType().IsArray;
        }
    }
}