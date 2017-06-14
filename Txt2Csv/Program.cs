using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Txt2Csv
{
    class Program
    {
        static void Main(string[] args)
        {
            var src = (args.Length == 0) ? @".\" : args[0];
            var dirs = Toolkit.DataAccessLayer.AllDirs(src);

            foreach (var dir in dirs)
            {
                var everything = Toolkit.DataAccessLayer.AllFiles(dir);
                foreach (var item in everything)
                {
                    if (Toolkit.DataAccessLayer.HaveSameEndings("inventory.txt", item))
                    {
                        string data = Converter.ConvertInventory(Toolkit.DataAccessLayer.LoadFile(item));
                        Toolkit.DataAccessLayer.SaveFile($"{item}.csv", data);
                    }
                    else if (Toolkit.DataAccessLayer.HaveSameEndings("results.txt", item))
                    {
                        string data = Converter.ConvertResults(Toolkit.DataAccessLayer.LoadFile(item));
                        Toolkit.DataAccessLayer.SaveFile($"{item}.csv", data);
                    }
                }
            }
        }
    }
}
