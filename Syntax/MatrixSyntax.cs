using System.Collections.Generic;
using System.Linq;
using Interpres.Tokens;

namespace Interpreter.Syntax
{
    public class Matrix : AbstractSyntax
    {
        private List<AbstractSyntax> elements;

        public Matrix(List<AbstractSyntax> elements)
        {
            this.elements = elements;
        }

        public override object GetValue()
        {
            return elements.Select(element => element.GetValue()).ToArray();
        }
    }
}