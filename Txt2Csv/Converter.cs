using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txt2Csv
{
    public class Converter
    {
        /// <summary>
        /// Converts a inventory file from text to a CSV table.
        /// </summary>
        /// <param name="inlet">The raw text.</param>
        /// <returns>The raw CSV table string.</returns>
        public static string ConvertInventory(string text)
        {
            string[] lines = text.Split('\n').Select(line => line.Trim()).ToArray();
            int groupSize = int.Parse(lines[0]);
            Queue<string> inlet = new Queue<string>(lines.Skip(1));
            Queue<string> outlet = new Queue<string>();

            while (inlet.Count > 1)
            {
                var line = "";
                for (int i = 0; i <= groupSize; ++i)
                {
                    var item = inlet.Dequeue();
                    line = (line.Length == 0) ? item : $"{line}\t{item}";
                }
                outlet.Enqueue(line);
            }

            return outlet.Aggregate("", (box, it) => $"{box}{it}\n");
        }

        /// <summary>
        /// Converts a results file from text to a CSV table.
        /// </summary>
        /// <param name="inlet">The raw file text.</param>
        /// <returns>The raw CSV table text.</returns>
        public static string ConvertResults(string text)
        {
            Queue<string> outlet = new Queue<string>();
            Queue<string> inlet = new Queue<string>(text.Split('\n')
                                                        .Select(line => line.Trim())
                                                        .Where(line => line.Length > 0));

            while (inlet.Count > 0)
            {
                var contents = inlet.Dequeue().Split(' ');
                var result = contents.Skip(2).Aggregate((box, it) => $"{box}{it} ").Trim();
                var line = $"{contents[0]}\t{contents[1]}\t{result}";
                outlet.Enqueue(line);
            }

            return outlet.Aggregate("", (box, it) => $"{box}{it}\n");
        }
    }
}
