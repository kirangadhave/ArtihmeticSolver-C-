using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticSolver {
    public class Token {
        public string Value { get; set; }
        public TokenType Type { get; set; }

        public bool Equals ( Token token ) {
            return ((Value == token.Value) && (Type == token.Type));
        }
    }
}
