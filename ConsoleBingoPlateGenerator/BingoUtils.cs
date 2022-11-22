using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBingoPlateGenerator
{
    public static class BingoUtils
    {
        public static bool CheckLength(this string target)
        {
            return target.Length == 5 || target.Length == 9;
        }

        public static bool CheckLength(this List<string> target)
        {
            foreach (string value in target)
            {
                if (value.Length == 9 || value.Length == 5)
                {
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}
