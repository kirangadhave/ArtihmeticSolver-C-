using ArithmeticSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticSolver {
    public static class Solver {
        public static double Solve(string equation, bool hasVariables = false, Dictionary<string, double> variableDictionary = null ) {

            if (equation == null)
                throw new ArgumentNullException(nameof(equation));
            if (hasVariables)
                if (variableDictionary == null || variableDictionary.Count == 0)
                    throw new ArgumentException($"{nameof(variableDictionary)} is null or empty");

            var lexer = new Lexer(equation, hasVariables);
            var tokens = lexer.Tokenize();

            if (tokens.ToList() == null)
                throw new Exception("Token List is null");

            var expression = ExpressionHandler.Build(tokens.ToList());
            var answer = expression.Solve(variableDictionary);
            return 0;
        }
    }
}
