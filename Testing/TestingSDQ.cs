using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{ 
    [TestFixture]
    public class TestingSDQ
    {
        [Test]
        public void TestFullProgram()
        {
            string[] args = { @"..\..\..\Data" };
            Assert.DoesNotThrow(() =>
            {
                SDQ.Program.Main(args); 
            });
        }
    }
}
