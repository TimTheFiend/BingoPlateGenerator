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




            int plateAmount = 100;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var plates = BingoFactory.CreatePlates(plateAmount);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine((float)elapsedMs / 1000 + " seconds.");

            string foo = plates.ElementAt(0);

            BingoPlate card = new BingoPlate(foo);

            //card.CreateBingoCard();
            Console.ReadLine();
        }

    }
}
