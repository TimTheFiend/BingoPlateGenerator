using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BingoPlateGenerator
{
    public static class BingoPrinter
    {
        private static readonly int PixelsBetweenRows = 18;
        private static readonly int StartY = 29;
        private static int TopY = 39;
        private static int MidY = TopY + PixelsBetweenRows;
        private static int BotY = MidY + PixelsBetweenRows;

        private static readonly int StartX = 19;
        private static readonly int PixelsBetweenCells = 33;

        private static readonly string TemplatePath = @"CardTemplate.png";
        //private static readonly string TemplatePath = @"C:\Users\KOM\Downloads\CardTemplate.png";
        private static readonly string OutputPath = @"C:\Users\KOM\Downloads\BingoCards";

        private static readonly PointF Title = new PointF(3, 7);
        private static Font arial = new Font("Arial", 10);

        public static void PrintPlates(IEnumerable<string> plates, string Title, string OutputDir, Bitmap cardTemplate)
        //public static void PrintPlates(IEnumerable<BingoPlate> plates, string Title, string OutputDir)
        {
            //Bitmap OriginalBitmap = (Bitmap)Image.FromFile(TemplatePath);
            int paddingLength = plates.Count().ToString().Length;
            int totalPlateAmount = plates.Count();
            int counter = 0;

            if (String.IsNullOrEmpty(Title))
            {
                Title = "Bingo dingo";
            }

            foreach (string plateId in plates)
            {
                BingoPlate plate = new BingoPlate(plateId);
                counter++;
                Bitmap tempBitmap = new Bitmap(cardTemplate);

                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    g.DrawString(Title, arial, System.Drawing.Brushes.Black, new PointF(0, 0));
                    g.DrawString($"{counter} of {totalPlateAmount}", new Font("Arial", 8.5f), System.Drawing.Brushes.Black, new PointF(0, 12.5f));
                    for (int y = 0; y < plate.Card.GetLength(0); y++)
                    {
                        for (int x = 0; x < plate.Card.GetLength(1); x++)
                        {
                            if (plate.Card[y, x] == 0)
                            {
                                continue;
                            }
                            PointF drawPos = new PointF(
                                x: StartX + (x * PixelsBetweenCells) - 5,
                                y: StartY + (y * PixelsBetweenRows)
                                );

                            g.DrawString(plate.Card[y, x].ToString(), arial, System.Drawing.Brushes.Blue, drawPos);
                        }
                    }
                }

                string savePath = Path.Combine(OutputDir, $"BingoCard_{counter.ToString().PadLeft(paddingLength, '0')}.png");
                //string savePath = Path.Combine(OutputPath, $"BingoCard_{counter.ToString().PadLeft(paddingLength, '0')}.png");
                tempBitmap.Save(savePath);
            }
        }
    }

}
