using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SNAP;

namespace Testing
{
    [TestFixture]
    public class TestingSNAP
    {
        public static string GetTestDirectory()
        {
            return @"C:\Users\neuro\Documents\Lab\src\github.com\lab-neuro-comp\Scorer\Data";
        }

        public static void DoNothing(object o)
        {

        }

        [Test]
        public void TestReadingScoresCorrectly()
        {
            // Not goint to test the data access layer since it is already thoroughly tested on the SDQ tests
            var everything = Toolkit.DataAccessLayer.AllFiles(GetTestDirectory());
            var results = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");
            foreach (var result in results)
            {
                var evaluator = new Evaluator();
                evaluator.Read(Toolkit.DataAccessLayer.LoadFile(result[0]), Toolkit.DataAccessLayer.LoadFile(result[1]));
                Assert.AreEqual(18, evaluator.Part1Answers.Length);
                Assert.AreEqual(4, evaluator.Part2Answers.Length);
            }
        }

        [Test]
        public void TestEvaluatingFirstPartCorrectly()
        {
            var everything = Toolkit.DataAccessLayer.AllFiles(GetTestDirectory());
            var results = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");
            foreach (var result in results)
            {
                var evaluator = new Evaluator();
                Assert.Throws<System.InvalidOperationException>(() => evaluator.Evaluate());
                evaluator.Read(Toolkit.DataAccessLayer.LoadFile(result[0]), Toolkit.DataAccessLayer.LoadFile(result[1]));
                Assert.Throws<System.InvalidOperationException>(() => DoNothing(evaluator.IsInattentive));
                evaluator.Evaluate();
                Assert.IsTrue(evaluator.IsInattentive);
            }
        }

        [Test]
        public void TestEvaluatingSecondPartCorrectly()
        {
            var everything = Toolkit.DataAccessLayer.AllFiles(GetTestDirectory());
            var results = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");
            foreach (var result in results)
            {
                var evaluator = new Evaluator();
                Assert.Throws<System.InvalidOperationException>(() => evaluator.Evaluate());
                evaluator.Read(Toolkit.DataAccessLayer.LoadFile(result[0]), Toolkit.DataAccessLayer.LoadFile(result[1]));
                Assert.Throws<System.InvalidOperationException>(() => DoNothing(evaluator.IsImpulsive));
                evaluator.Evaluate();
                Assert.IsTrue(evaluator.IsImpulsive);
            }
        }

        [Test]
        public void TestEvaluatingCombinationCorrectly()
        {
            var everything = Toolkit.DataAccessLayer.AllFiles(GetTestDirectory());
            var results = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");
            foreach (var result in results)
            {
                var evaluator = new Evaluator();
                evaluator.Read(Toolkit.DataAccessLayer.LoadFile(result[0]), Toolkit.DataAccessLayer.LoadFile(result[1]));
                Assert.Throws<System.InvalidOperationException>(() => DoNothing(evaluator.IsCombined));
                evaluator.Evaluate();
                Assert.IsTrue(evaluator.IsCombined);
            }
        }

        [Test]
        public void TestIfTestExecutionIsValidBasedOnPart2()
        {
            var everything = Toolkit.DataAccessLayer.AllFiles(GetTestDirectory());
            var results = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");
            foreach (var result in results)
            {
                var evaluator = new Evaluator();
                evaluator.Read(Toolkit.DataAccessLayer.LoadFile(result[0]), Toolkit.DataAccessLayer.LoadFile(result[1]));
                Assert.Throws<System.InvalidOperationException>(() => DoNothing(evaluator.IsValid));
                evaluator.Evaluate();
                Assert.IsFalse(evaluator.IsValid);
            }
        }

        // TODO Test full evaluation
    }
}
