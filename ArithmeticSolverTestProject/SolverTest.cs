using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArithmeticSolver;

namespace ArithmeticSolverTestProject {
    [TestClass]
    public class SolverTest {
        [TestMethod]
        public void TestSolver () {
            Solver.Solve("2 + (3*2)");
        }
    }
}
