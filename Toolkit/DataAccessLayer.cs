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
        public static string[] AllFiles(string src)
        {
            return Directory.GetFiles(src);
        }

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

        public static string LoadFile(string filename)
        {
            return (File.Exists(filename))? File.ReadAllText(filename) : null;
        }
    }
}
