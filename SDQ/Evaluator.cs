using System.Linq;

namespace SDQ
{
    public class Evaluator
    {
        #region Constructor
        /// <summary>
        /// Creates a new Evaluator, without any information to be used yet.
        /// </summary>
        public Evaluator()
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Reads a raw output table from Scalemate and gets the anwers given by the subject.
        /// </summary>
        /// <param name="raw">The raw text in the </param>
        /// <returns></returns>
        public static int[] ExtractAnswers(string raw)
        {
            return raw.Split('\n')[2].Split('\t')
                      .Where(it => it.Length > 0)
                      .Select(it => int.Parse(it))
                      .ToArray();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Extracts the needed information from the raw Scalemate table text.
        /// </summary>
        /// <param name="raw">The text in the output Scalemate table in a string.</param>
        public void Read(string raw)
        {
            Text = raw;
            Part1Answers = ExtractAnswers(Text);
        }

        /// <summary>
        /// Extracts the needed information from the raw Scalemate table text.
        /// </summary>
        /// <param name="part1">The text in the first output Scalemate table in a string.</param>
        /// <param name="part2">The text in the second output Scalemate table in a string.</param>
        public void Read(string part1, string part2)
        {
            Read(part1);
            Part2Answers = ExtractAnswers(part2);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The raw table text.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// The answers, as given by the subject, in the first part.
        /// </summary>
        public int[] Part1Answers { get; private set; }
        /// <summary>
        /// The answers, as given by the subject, in the second part.
        /// </summary>
        public int[] Part2Answers { get; private set; }
        #endregion
    }
}
