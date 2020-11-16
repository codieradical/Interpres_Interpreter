using Interpres.Lexer.Tokens;
using Interpres.Lexer.Tokens.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interpres_dev
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diag, AbstractToken root, Type eofToken)
        {
            Diag = diag.ToArray();
            Root = root;
            EOFToken = eofToken;
        }

        public IReadOnlyList<string> Diag { get; }
        public AbstractToken Root { get; }
        public Type EOFToken { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);

            return parser.Parse();
        }
    }

}
