using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    class TestingTxt2Csv
    {
        [Test]
        public void TestIfQueueReturnsNullOrBreaks()
        {
            string random = "this is a random string with spaces";
            Queue<string> queue = new Queue<string>(random.Split(' '));
            
            while (true)
            {
                try
                {
                    string item = queue.Dequeue();
                }
                catch (InvalidOperationException e)
                {
                    break;
                }
            }

            Assert.AreEqual(0, queue.Count);
        }
    }
}
