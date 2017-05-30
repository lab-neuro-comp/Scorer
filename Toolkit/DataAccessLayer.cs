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
            return (s.Length > t.Length) ?
                s.Reverse().ToString().StartsWith(t.Reverse().ToString()) :
                t.Reverse().ToString().StartsWith(s.Reverse().ToString());
        }

        public static string LoadFile(string filename)
        {
            return (File.Exists(filename))? File.ReadAllText(filename) : null;
        }
    }
}
