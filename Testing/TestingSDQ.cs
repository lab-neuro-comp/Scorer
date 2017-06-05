using NUnit.Framework;
using System;

namespace Testing
{
    [TestFixture]
    public class TestingSDQ
    {
        public static string GetDirectory()
        {
            return @"C:\Users\neuro\Documents\Lab\src\github.com\lab-neuro-comp\Scorer\Data";
        }

        [Test]
        public void TestToGetAllFilesInValidDirectory()
        {
            string directory = TestingSDQ.GetDirectory();
            string[] files = Toolkit.DataAccessLayer.AllFiles(directory);
            Assert.AreEqual(4, files.Length);
        }

        [Test]
        public void TestIfStringEndingMatchingWorks()
        {
            var sdqEnding = "sdq-1.csv";
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            Assert.IsTrue(Toolkit.DataAccessLayer.HaveSameEndings(sdqEnding, files[0]));
            Assert.IsTrue(Toolkit.DataAccessLayer.HaveSameEndings(files[0], sdqEnding));
        }
        
        [Test]
        public void TestLoadingFiles()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            var sdqEnding = "sdq-1.csv";
            Assert.IsNull(Toolkit.DataAccessLayer.LoadFile("randomname.asdf"));
            foreach (var file in files)
            {
                if (Toolkit.DataAccessLayer.HaveSameEndings(sdqEnding, file))
                {
                    Assert.NotNull(Toolkit.DataAccessLayer.LoadFile(file));
                }
            }
        }

        [Test]
        public void TestLoadedCorrectFiles()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            var sdqEnding = "sdq-1.csv";
            foreach (var file in files)
            {
                if (Toolkit.DataAccessLayer.HaveSameEndings(sdqEnding, file))
                {
                    var text = Toolkit.DataAccessLayer.LoadFile(file);
                    var evaluator = new SDQ.Evaluator();
                    evaluator.Read(text);
                    Assert.AreEqual(25, evaluator.Part1Answers.Length);
                }
            }
        }

        [Test]
        public void TestFileGroupingsByTestType()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            string[][] groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "sdq");
            foreach (var grouping in groupings)
            {
                Assert.AreEqual(2, grouping.Length);
            }
            groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "snap");
            foreach (var grouping in groupings)
            {
                Assert.AreEqual(2, grouping.Length);
            }
        }

        [Test]
        public void TestSdqExecutionProcessing()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            string[][] groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "sdq");
            int[] expectedAnswers = new int[] { 17, 4, 9, 3, 6, 0 };
            int[] expectedBehaviours = new int[] { 1, 0, 2, 0, 2, 2 };
            foreach (var grouping in groupings)
            {
                var evaluator = new SDQ.Evaluator();
                // Reading data
                var part1 = Toolkit.DataAccessLayer.LoadFile(grouping[0]);
                var part2 = Toolkit.DataAccessLayer.LoadFile(grouping[1]);
                Assert.Throws<System.InvalidOperationException>(() => evaluator.Calculate());
                evaluator.Read(part1, part2);
                Assert.AreEqual(25, evaluator.Part1Answers.Length);
                Assert.AreEqual(7, evaluator.Part2Answers.Length);

                // Evaluating test execution
                var givenAnswers = evaluator.GroupAnswers();
                var resultingBehaviours = evaluator.Calculate();
                for (int i = 0; i < resultingBehaviours.Length; ++i)
                {
                    Assert.AreEqual(expectedAnswers[i], givenAnswers[i]);
                    Assert.AreEqual(expectedBehaviours[i], resultingBehaviours[i]);
                }
            }
        }
    }
}
