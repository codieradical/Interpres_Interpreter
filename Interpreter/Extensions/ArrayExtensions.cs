using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static T[] SubArray<T>(this T[] data, int index)
        {
            T[] result = new T[data.Length - index];
            Array.Copy(data, index, result, 0, data.Length - index);
            return result;
        }

        public static string ToString(this object[] array)
        {
            string[] valueStrings = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                valueStrings[i] = array[i].ToString();
                if (array[i].GetType().IsArray)
                    valueStrings[i] = ((object[])array[i]).ToString();
            }
            return "[" + string.Join(", ", valueStrings) + "]";
        }
    }
}
