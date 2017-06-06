using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAP
{
    /// <summary>
    /// Evaluates the results 
    /// </summary>
    public class Evaluator
    {
        /// <summary>
        /// Creates a new evaluator.
        /// </summary>
        public Evaluator()
        {

        }

        /// <summary>
        /// Translates the contens in the SNAP output files into results,
        /// stored in the `Part1Answers` and `Part2Answers` properties.
        /// </summary>
        /// <param name="r1">The contents of the first file.</param>
        /// <param name="r2">The contents of the second file.</param>
        public void Read(string r1, string r2)
        {
            Part1Answers = this.ExtractAnswers(r1);
            Part2Answers = this.ExtractAnswers(r2);
        }

        /// <summary>
        /// Converts the text file string into meaningful results.
        /// </summary>
        /// <param name="text">The string in the file.</param>
        /// <returns>The answers</returns>
        protected int[] ExtractAnswers(string text)
        {
            return text.Split('\n')[1].Split('\t').Where(it => it.Length > 0).Select(int.Parse).ToArray();
        }

        /// <summary>
        /// Stores the answers in the first part of the test.
        /// </summary>
        public int[] Part1Answers { get; private set; } = null;
        /// <summary>
        /// Stores the answers in the second part of the test.
        /// </summary>
        public int[] Part2Answers { get; private set; } = null;
    }
}
