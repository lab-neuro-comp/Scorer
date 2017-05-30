using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Answers = ExtractAnswers(Text);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The raw table text.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// The answers, as given by the subject.
        /// </summary>
        public int[] Answers { get; private set; }
        #endregion
    }
}
