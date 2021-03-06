﻿using NUnit.Framework;
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
        public void TestIfStringEndingMatchingWorks()
        {
            Assert.IsTrue(Toolkit.DataAccessLayer.HaveSameEndings("joe frank", "frank"));
            Assert.IsTrue(Toolkit.DataAccessLayer.HaveSameEndings("frank", "joe frank"));
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
            string[][] groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "sdq-");
            foreach (var grouping in groupings)
            {
                Assert.AreEqual(2, grouping.Length);
            }
            groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "snap-");
            foreach (var grouping in groupings)
            {
                Assert.AreEqual(2, grouping.Length);
            }
        }

        [Test]
        public void TestSdqExecutionProcessing()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            string[][] groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "sdq-");
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

        [Test]
        public void TestGenerationOfOutputTableName()
        {
            string[] files = Toolkit.DataAccessLayer.AllFiles(TestingSDQ.GetDirectory());
            string[][] groupings = Toolkit.DataAccessLayer.GroupFilesByTest(files, "sdq-");
            string[] expectedResults = new string[] { "Teste2_sdq.csv", "Teste_sdq.csv" };
            for (int i = 0; i < groupings.Length; ++i)
            {
                var grouping = groupings[i];
                string completeOutputFileName = Toolkit.Prettifier.GenerateOutput(grouping);
                string[] outputFileNameParts = completeOutputFileName.Split('\\');
                string outputFileName = outputFileNameParts[outputFileNameParts.Length-1];
                Assert.AreEqual(expectedResults[i], outputFileName);
            }
        }
    }
}
