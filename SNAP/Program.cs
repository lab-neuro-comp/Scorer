using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var src = (args.Length == 0) ? @".\" : args[0];
            var everything = Toolkit.DataAccessLayer.AllFiles(src);
            var snapOnly = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "snap-");

            foreach (var group in snapOnly)
            {
                var evaluator = new Evaluator();

                // TODO Read data
                // TODO Process data
                // TODO Log info
            }

            Console.ReadLine();
        }
    }
}
