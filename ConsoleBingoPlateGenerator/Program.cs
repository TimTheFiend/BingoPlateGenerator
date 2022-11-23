using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleBingoPlateGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {



            int plateAmount = 1000000;

            var plates = BingoFactory.CreatePlates(plateAmount);

            List<BingoPlate> bingoCards = new List<BingoPlate>();
            foreach (string id in plates)
            {
                bingoCards.Add(new BingoPlate(id));
            }

            //BingoPlatePrinter.PrintPlates(bingoCards);
        }

        static void DrawOnImage()
        {
            string path = @"C:\Users\KOM\Downloads\Misc\lmaocat.png";
            Bitmap bitmap = (Bitmap)Image.FromFile(path);
            Bitmap tempBitmap = new Bitmap(bitmap);

            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                using (Font arialFont = new Font("Arial", 10))
                {
                    g.DrawString("lmao", arialFont, Brushes.Blue, new PointF(10f, 10f));

                }
                //Font font = new Font("Arial", 10);
            }


            //using (Graphics graphics = Graphics.FromImage(bitmap))
            //{
            //    using (Font arialFont = new Font("Arial", 10))
            //    {
            //        graphics.DrawString("lmao", arialFont, Brushes.Blue, new PointF(10f, 10f));
            //    }
            //}
            tempBitmap.Save(@"C:\Users\KOM\Downloads\Misc\lmaocat_1.png");
        }

    }
}
