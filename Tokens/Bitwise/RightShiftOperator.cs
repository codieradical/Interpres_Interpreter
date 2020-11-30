﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens.Bitwise
{
    class RightShiftOperator : AbstractOperator
    {
        public override string GetInputString()
        {
            return ">>";
        }

        public override ValueToken Operate(params ValueToken[] values)
        {
            if (values.Length < 1)
                throw new InvalidOperationException("Not enough values provided.");
            else if (values.Length > 2)
                throw new InvalidOperationException("Too many values provided.");

            if (!values[0].IsNumeric() || values[0].IsFloat())
                throw new ArgumentException($"Can't perform ${GetInputString()} operation on a {values[0].Value.GetType()}");

            if (!values[1].IsNumeric() || values[1].IsFloat())
                throw new ArgumentException($"Can't perform ${GetInputString()} operation by a {values[1].Value.GetType()}");


            dynamic l = values[0].Value;
            dynamic r = values[1].Value;

            return new ValueToken(l >> r);
        }
    }
}
