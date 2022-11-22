using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleBingoPlateGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var plates = BingoFactory.CreatePlates(10000);

        }

    }
}
