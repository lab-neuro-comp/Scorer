using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class DataAccessLayer
    {
        /// <summary>
        /// Get all files in the given directory. Does not check inside subdirectories.
        /// </summary>
        /// <param name="src">The directory to get the files</param>
        /// <returns>An array of file and dir names</returns>
        public static string[] AllFiles(string src)
        {
            return Directory.GetFiles(src);
        }

        /// <summary>
        /// Check if a string is the end of the other. Order does not matter here.
        /// </summary>
        /// <param name="s">First string</param>
        /// <param name="t">Second string</param>
        /// <returns>The promised comparison.</returns>
        public static bool HaveSameEndings(string s, string t)
        {
            int sl = s.Length;
            int tl = t.Length;
            int limit = (sl < tl) ? sl : tl;
            bool flag = true;

            for (int i = 0; i < limit; ++i)
            {
                if (s[sl-i-1] != t[tl-i-1])
                {
                    flag = false;
                }
            }

            return flag;
        }

        /// <summary>
        /// Loads the given file in memory.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>The file contents, or null if the file does not exist.</returns>
        public static string LoadFile(string filename)
        {
            return (File.Exists(filename))? File.ReadAllText(filename) : null;
        }
    }
}
