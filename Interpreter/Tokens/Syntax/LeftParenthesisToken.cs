﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    class LeftParenthesisToken : AbstractToken
    {
        public override string GetInputString()
        {
            return "(";
        }
    }
}
