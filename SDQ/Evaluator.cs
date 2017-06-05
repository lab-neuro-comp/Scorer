using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Calculates the results for the test execution. Can only be called after reading the
        /// data, else it will throw a `System.InvalidOperationException`.
        /// </summary>
        /// <returns>An array coding the result in each category. 0 means it is a normal result,
        /// 1 means it is a limiting result, and 2 means it is an anormal result. They follow
        /// this order: total dificulties, emotional symptoms, behaviour problems, hyperactivity,
        /// relationship problems and pro-social behaviour.</returns>
        public int[] Calculate()
        {
            int[] groupsOfAnswers = this.GroupAnswers();
            List<int> results = new List<int>();

            // Total results
            int total = groupsOfAnswers[0];
            results.Add((total >= 0 && total <= 15) ? 0 : (total >= 16 && total <= 19) ? 1 : 2 );
            // Emotional symptoms
            total = groupsOfAnswers[1];
            results.Add((total >= 0 && total <= 5) ? 0 : (total >= 6 && total <= 6) ? 1 : 2);
            // Behaviour problems
            total = groupsOfAnswers[2];
            results.Add((total >= 0 && total <= 3) ? 0 : (total >= 4 && total <= 4) ? 1 : 2);
            // Hyperactivity
            total = groupsOfAnswers[3];
            results.Add((total >= 0 && total <= 5) ? 0 : (total >= 6 && total <= 9) ? 1 : 2);
            // Relationship problems
            total = groupsOfAnswers[4];
            results.Add((total >= 0 && total <= 3) ? 0 : (total >= 4 && total <= 5) ? 1 : 2);
            // Prosocial behaviour
            total = groupsOfAnswers[0];
            results.Add((total >= 6 && total <= 10) ? 0 : (total >= 5 && total <= 5) ? 1 : 2);

            return results.ToArray();
        }

        /// <summary>
        /// Groups the given answers into their own categories, as expected by the test. Throws a
        /// `System.InvalidOperationException` if the evaluator has not read the data yet or if
        /// the data is in wrong conditions.
        /// </summary>
        /// <returns>An array coding the result in each category. They follow this order: total 
        /// dificulties, emotional symptoms, behaviour problems, hyperactivity, relationship 
        /// problems and pro-social behaviour.</returns>
        public int[] GroupAnswers()
        {
            if ((Part1Answers == null) || (Part1Answers.Length != 25))
                throw new System.InvalidOperationException();
            var total = Part1Answers.Take(20).ToArray();
            var emo = new int[] { Part1Answers[2], Part1Answers[7], Part1Answers[12],
                                  Part1Answers[15], Part1Answers[23] };
            var beh = new int[] { Part1Answers[4], Part1Answers[6], Part1Answers[11],
                                  Part1Answers[17], Part1Answers[21] };
            var hyp = new int[] { Part1Answers[1], Part1Answers[9], Part1Answers[14],
                                  Part1Answers[20], Part1Answers[24] };
            var rel = new int[] { Part1Answers[5], Part1Answers[10], Part1Answers[13],
                                  Part1Answers[18], Part1Answers[22] };
            var soc = new int[] { Part1Answers[0], Part1Answers[3], Part1Answers[8],
                                  Part1Answers[16], Part1Answers[19] };
            var outlet = new int[][] { total, emo, beh, hyp, rel, soc };
            return outlet.Select(it => it.Sum()).ToArray();
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
        public int[] Part1Answers { get; private set; } = null;
        /// <summary>
        /// The answers, as given by the subject, in the second part.
        /// </summary>
        public int[] Part2Answers { get; private set; }
        #endregion
    }
}
