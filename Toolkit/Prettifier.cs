﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    /// <summary>
    /// Makes data readable to humans. Excels at creating tables.
    /// </summary>
    public class Prettifier
    {
        /// <summary>
        /// Generates the output file name for the given grouping
        /// </summary>
        /// <param name="grouping">A test execution output grouping</param>
        /// <returns>The output file name for the given subject-test pair</returns>
        public static string GenerateOutput(string[] grouping)
        {
            var parts = grouping[0].Split('_');
            return $"{parts[0]}_{parts[1].Split('-')[0]}.csv";
        }

        /// <summary>
        /// Generates the output table for the SDQ test.
        /// </summary>
        /// <param name="answers">The sum of the answers for each category.</param>
        /// <param name="behaviours">The coded result for each behaviour.</param>
        /// <returns>A TSV table.</returns>
        public static string MakeSdqLegible(int[] answers, int[] behaviours)
        {
            Queue<string> lines = new Queue<string>();
            int limit = answers.Length;
            string[] tags = new string[] 
            {
                "Total",
                "Sintomas Emocionais",
                "Problemas de Comportamento",
                "Hiperatividade",
                "Problemas de Relacionamento",
                "Comportamento Pró-Social"
            };

            for (int i = 0; i < limit; ++i)
            {
                var behaviour = (behaviours[i] == 0) ? "Normal" : (behaviours[i] == 1) ? "Limítrofe" : "Anormal";
                lines.Enqueue($"{tags[i]}\t{answers[i]}\t{behaviour}");
            }

            return lines.Aggregate("", (box, it) => $"{box}{it}\n");
        }

        // TODO Make SNAP output test legible
    }
}