using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class TestingSDQ
    {
        public static string GetDirectory()
        {
            return @"Scorer\Data";
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
            foreach (var grouping in groupings)
            {
                var evaluator = new SDQ.Evaluator();
                evaluator.Read(grouping[0]);
                if (grouping.Length > 1)
                {
                    evaluator.Read(grouping[1]);
                }
                // TODO Evaluate test execution to provide a test case here
                Assert.IsTrue(false);
            }
        }
    }
}
