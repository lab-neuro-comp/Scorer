﻿using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// Groups a list of files into similar test executions.
        /// </summary>
        /// <param name="testType">The test to look for.</param>
        /// <returns>An array as each item is an array of the test executions
        /// of the same test by the same subject.</returns>
        public static string[][] GroupFilesByTest(string[] files, string testType)
        {
            Dictionary<string, List<string>> groupings = new Dictionary<string, List<string>>();
            List<string[]> outlet = new List<string[]>();

            // Populating groupings by subject
            foreach (var file in files)
            {
                var subject = file.Split('_')[0];
                var test = file.Split('_')[1];
                if (test.StartsWith(testType))
                {
                    if (!groupings.ContainsKey(subject))
                    {
                        groupings[subject] = new List<string>();
                    }
                    groupings[subject].Add(file);
                }
            }

            // Making abstract groupings more sparse
            foreach (var subjects in groupings.Keys)
            {
                outlet.Add(groupings[subjects].ToArray());
            }

            return outlet.ToArray();
        }
    }
}
