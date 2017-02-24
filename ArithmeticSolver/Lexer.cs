using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArithmeticSolver {
    public class Lexer {
        public string Equation { get; set; }
        bool HasVariables { get; set; }
        Token Current { get; set; }
        InputStream IS { get; set; }

        public Lexer (string equation, bool hasVariables) {
            Equation = equation;
            HasVariables = hasVariables;
            IS = new InputStream(Equation);
        }

        public IEnumerable<Token> Tokenize () {
            var tokens = new List<Token>();
            if (Equation == null)
                throw new Exception("Equation string is null");

            do {
                try {
                    tokens.Add(Next());
                } catch(Exception ex) { Debug.WriteLine(ex); }
            } while (!EOF);


            return tokens;
        }

        #region Read Functions
        Token ReadNext () {
            ReadWhile( (c) => {
                    return " \t\n".Contains(c.ToString());
                });

            if (IS.EOF)
                return null;
            var ch = IS.Peek;

            if (IsDigit(ch))
                return ReadDigit();

            if (VarStart(ch))
                return ReadVar(ch);

            if (IsOperator(ch))
                return ReadOperator(ch);

            if (ch == '(')
                return new Token { Type = TokenType.StartParenthesis, Value = ReadWhile((c)=> { return c == '('; }, true) };

            if (ch == ')')
                return new Token { Type = TokenType.EndParenthesis, Value = ReadWhile(( c ) => { return c == ')'; }, true) };

            IS.Err($"Cannot handle character {ch}");

            return null;
        }
        string ReadWhile ( Func<char, bool> predicate, bool one = false ) {
            var builder = new StringBuilder();
            builder.Append("");
            if (!one) {
                while (!IS.EOF && predicate.Invoke(IS.Peek)) {
                    builder.Append(IS.Next());
                }
            }
            if (one && predicate.Invoke(IS.Peek)) {
                builder.Append(IS.Next());
            }
            return builder.ToString();
        }



        #region Read Operators
        Token ReadOperator ( char ch ) {
            return new Token { Value = ReadWhile(IsOperator), Type = TokenType.Operator };
        }

        bool IsOperator ( char ch ) {
            return @"+-/*".Contains(ch.ToString());
        }
        #endregion

        #region Read Variables
        Token ReadVar ( char ch ) {
            var id = ReadWhile(( c ) => {
                return VarStart(c) || Regex.IsMatch(c.ToString(),@"\d");
            });
            return new Token { Value = id, Type = TokenType.Variable };
        }

        bool VarStart ( char ch ) {
            return Regex.IsMatch(ch.ToString(), @"[a-z]");
        }
        #endregion

        #region Read Numbers
        Token ReadDigit () {
            var hasDecimal = false;
            var number = ReadWhile(( c ) => {
                if(c == '.') {
                    if (hasDecimal)
                        return false;
                    hasDecimal = true;
                    return true;
                }
                return IsDigit(c);
            });

            double num;
            if (double.TryParse(number, out num))
                return new Token { Value = number, Type = TokenType.Number };
            return null;
        }

        bool IsDigit ( char ch ) {
            return Regex.IsMatch(ch.ToString(), @"\d");
        }
        #endregion
        #endregion

        #region Traversing Functions
        Token Next () {
            var tok = Current;
            Current = null;
            if (tok == null)
                tok = ReadNext();
            return tok;
        }

        Token Peek () {
            if (Current == null)
                Current = ReadNext();
            return Current;
        }

        bool EOF => (Peek() == null);
        #endregion
    }

    class InputStream {
        public string Equation { get; set; }
        public int Position { get; set; }
        public int Col { get; set; }
        public int Line { get; set; }

        public InputStream (string equation) {
            Equation = equation;
            Line = 1;
        }

        #region Traversing Functions
        public char Next () {
            var ch = Equation[Position++];
            if(ch == '\n') {
                Line++;
                Col = 0;
            } else {
                Col++;
            }
            return ch;
        }

        public char Peek => Equation[Position];

        public bool EOF => (Position == Equation.Length);

        public void Err( string message ) {
            throw new Exception(message);
        }
        #endregion
    }
}
