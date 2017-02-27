using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArithmeticSolver.Models {
    public static class ExpressionHandler {

        /// <summary>
        /// Takes Tokens as input to build a Binary Expression.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>A Binary Expression object</returns>
        public static IExpression Build ( IEnumerable<Token> tokens ) {
            var exp = default(IExpression);

            var parDetected = 0;
            foreach (var token in tokens) {
                switch (token.Type) {
                    case TokenType.StartParenthesis: {
                            parDetected++;
                            break;
                        }
                    case TokenType.EndParenthesis: {
                            if (parDetected != 0)
                                parDetected--;
                            break;
                        }
                    case TokenType.Operator: {
                            if (parDetected == 0) {
                                var index = tokens.ToList().IndexOf(token);
                                var left = tokens.Take(index);
                                var right = tokens.Skip(index + 1);
                                exp = new Expression {
                                    Operator = new Operator { Token = token },
                                    Left = Build(left),
                                    Right = Build(right)
                                };
                            }
                            break;
                        }
                    case TokenType.Variable:
                    case TokenType.Number: {
                            if (parDetected == 0)
                                if (tokens.Count() == 1)
                                    exp = new Term { Token = token };
                            break;
                        }
                }
            }
            
            if (tokens.First().Value == "(" && tokens.Last().Value == ")") {
                var list = tokens.ToList();
                list.Remove(list.First());
                list.Remove(list.Last());
                var s = string.Join("", list.Select(x => x.Value));
                if(Regex.IsMatch(s, @".*\(.*\).*") || !(s.Contains("(") && s.Contains(")")))
                    exp = Build(list);
            }

            return exp;
        }

        /// <summary>
        /// Recursively solves the binary expression and gives out double as a result.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="valueDict"></param>
        /// <returns></returns>
        public static double Solve( this IExpression expression, Dictionary<string, double> valueDict) {
            if(expression is Term) {
                var term = expression as Term;
                switch (term.Token.Type) {
                    case TokenType.Number: {
                            return double.Parse(term.Token.Value);
                        }
                    case TokenType.Variable: {
                            return valueDict[term.Token.Value];
                        }
                }
            }

            if(expression is Expression) {
                var exp = expression as Expression;
                    return Evaluate(Solve(exp.Left, valueDict), exp.Operator, Solve(exp.Right, valueDict));
                
            }

            throw new Exception("Cannot Calculate");
        }

        static double Evaluate ( double left, Operator op, double right ) {
            
            switch (op.Token.Value) {
                case "+":
                        return left + right;
                case "-":
                        return left - right;
                case "*":
                        return left * right;
                case "/": {
                        if (right.IsZero())
                            return left / right;
                        throw new ArgumentException("Right Hand Side value evaluates to 0");
                    } 
                default:
                    throw new ArgumentException("Wrong Operator");
            }
        }

        static bool IsZero( this double d ) => Math.Abs(d - 0.0) > 0.0001;
    }
}
