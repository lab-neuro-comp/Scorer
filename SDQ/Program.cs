using System;
using System.Linq;

namespace SDQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var src = (args.Length == 0) ? @".\" : args[0];
            var everything = Toolkit.DataAccessLayer.AllFiles(src);
            var sdqOnly = Toolkit.DataAccessLayer.GroupFilesByTest(everything, "sdq-");

            foreach (var group in sdqOnly)
            {
                // Reading data
                var evaluator = new Evaluator();
                if (group.Length == 1)
                {
                    evaluator.Read(Toolkit.DataAccessLayer.LoadFile(group[0]));
                }
                else
                {
                    evaluator.Read(Toolkit.DataAccessLayer.LoadFile(group[0]),
                                   Toolkit.DataAccessLayer.LoadFile(group[1]));
                }

                // Generating output table
                var givenAnswers = evaluator.GroupAnswers();
                var resultingBehaviours = evaluator.Calculate();
                var output = Toolkit.Prettifier.GenerateOutput(group);
                var data = Toolkit.Prettifier.MakeSdqLegible(givenAnswers, resultingBehaviours);
                Toolkit.DataAccessLayer.SaveFile(output, data);

                // Logging
                Console.WriteLine($"{group[0].Split('_')[0]}:");
                Console.WriteLine(resultingBehaviours.Aggregate("", (box, it) => $"{box}- {it}\n"));
            }
        }
    }
}
