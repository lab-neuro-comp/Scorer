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
    }
}
