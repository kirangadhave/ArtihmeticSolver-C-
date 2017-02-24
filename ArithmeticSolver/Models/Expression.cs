using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticSolver.Models {
    public class Expression : IExpression {
        public IExpression Left { get; set; }
        public Operator Operator { get; set; }
        public IExpression Right { get; set; }
    }
}
