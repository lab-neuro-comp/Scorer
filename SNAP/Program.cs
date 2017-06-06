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
                // Reading data
                var evaluator = new Evaluator();
                var text1 = Toolkit.DataAccessLayer.LoadFile(group[0]);
                var text2 = Toolkit.DataAccessLayer.LoadFile(group[1]);
                var output = Toolkit.Prettifier.GenerateOutput(group);
                evaluator.Read(text1, text2);
                evaluator.Evaluate();

                // Writting data
                var data = new bool[]
                {
                    evaluator.IsImpulsive,
                    evaluator.IsInattentive,
                    evaluator.IsValid
                };
                var content = Toolkit.Prettifier.MakeSnapLegible(data);
                Toolkit.DataAccessLayer.SaveFile(output, content);
            }
        }
    }
}
