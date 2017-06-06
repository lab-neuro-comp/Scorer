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

        #region Methods
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
        /// Analyzes the given results.
        /// </summary>
        public void Evaluate()
        {
            // Checking if state is valid
            if (Part1Answers == null)
            {
                throw new InvalidOperationException();
            }

            // Proceeding with evaluation
            IsInattentive = Part1Answers.Take(10).Where(it => it >= 2).Count() >= 6;
            IsImpulsive = Part1Answers.Skip(10).Where(it => it >= 2).Count() >= 6;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Stores the answers in the first part of the test.
        /// </summary>
        public int[] Part1Answers { get; private set; } = null;
        /// <summary>
        /// Stores the answers in the second part of the test.
        /// </summary>
        public int[] Part2Answers { get; private set; } = null;
        /// <summary>
        /// Tells if the evaluator assessed if the child experiences predominantly inattentive ADHD.
        /// </summary>
        public bool IsInattentive
        {
            get
            {
                if (_IsInattentive_ == 0)
                    throw new InvalidOperationException();
                else
                    return _IsInattentive_ > 0;
            }
            private set
            {
                _IsInattentive_ = (byte) ((value) ? 1 : -1);
            }
        }
        private byte _IsInattentive_ { get; set; } = 0;
        /// <summary>
        /// Tells if the evaluator assessed if the child experiences predominantly impulsive ADHD.
        /// </summary>
        public bool IsImpulsive
        {
            get
            {
                if (_IsInattentive_ == 0)
                    throw new InvalidOperationException();
                else
                    return _IsInattentive_ > 0;
            }
            private set
            {
                _IsImpulsive_ = (byte)((value) ? 1 : -1);
            }
        }
        private byte _IsImpulsive_ { get; set; } = 0;
        /// <summary>
        /// Tells if the evaluator assessed if the child experiences predomnantly combined ADHD.
        /// </summary>
        public bool IsCombined { get { return IsImpulsive && IsInattentive; } }
        #endregion
    }
}
