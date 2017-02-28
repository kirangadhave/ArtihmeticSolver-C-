using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArithmeticSolver;

namespace ArithmeticSolverTestProject {
    [TestClass]
    public class SolverTest {
        //[TestMethod]
        //public void SimpleAdditionTest () {
        //    var expected = Solver.Solve("2+3");
        //    Assert.AreEqual(expected, 5, 0.001, "Addition Failed");
        //}

        //[TestMethod]
        //public void SimpleSubtractionTest () {
        //    var expected = Solver.Solve("2-3");
        //    Assert.AreEqual(expected, -1, 0.001, "Subtraction Failed");
        //}

        //[TestMethod]
        //public void SimpleMultiplicationTest () {
        //    var expected = Solver.Solve("2*3");
        //    Assert.AreEqual(expected, 6, 0.001, "Multiplication Failed");
        //}

        //[TestMethod]
        //public void SimpleDivisionTest () {
        //    var expected = Solver.Solve("6/3");
        //    Assert.AreEqual(expected, 2, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void DivisionByZeroTest () {
        //    var expected = Solver.Solve("2/0");
        //}

        //[TestMethod]
        //public void BodmasTest1 () {
        //    var expected = Solver.Solve("2+6/3");
        //    Assert.AreEqual(expected, 4, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //public void BodmasTest2 () {
        //    var expected = Solver.Solve("2-6/3");
        //    Assert.AreEqual(expected, 0, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //public void BodmasTest3 () {
        //    var expected = Solver.Solve("4.5+6*2");
        //    Assert.AreEqual(expected, 16.5, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //public void BodmasTest4 () {
        //    var expected = Solver.Solve("14.5-6*2");
        //    Assert.AreEqual(expected, 2.5, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //public void ParanthesisTest1 () {
        //    var expected = Solver.Solve("(4.5+6)*2");
        //    Assert.AreEqual(expected, 21, 0.001, "Division Failed");
        //}

        //[TestMethod]
        //public void ParanthesisTest2 () {
        //    var expected = Solver.Solve("(4.5+6)*(2+3.3) - 23");
        //    Assert.AreEqual(expected, 32.65, 0.001, "Division Failed");
        //}

        [TestMethod]
        public void ParanthesisTest3 () {
            var expected = Solver.Solve(@"(2+1)/(3.3+1)");
            Assert.AreEqual(expected, 9.893939, 0.001, "Division Failed");
        }

    }
}
