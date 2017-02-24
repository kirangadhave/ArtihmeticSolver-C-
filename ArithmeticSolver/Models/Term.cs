using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticSolver.Models {
    public class Term : IExpression {
        public Token Token { get; set; }
    }
}
