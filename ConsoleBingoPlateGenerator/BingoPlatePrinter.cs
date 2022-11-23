using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBingoPlateGenerator
{
    public static class BingoPlatePrinter
    {
        private static readonly int PixelsBetweenRows = 18;
        private static readonly int StartY = 29;
        private static int TopY = 39;
        private static int MidY = TopY + PixelsBetweenRows;
        private static int BotY = MidY + PixelsBetweenRows;

        private static readonly int StartX = 19;
        private static readonly int PixelsBetweenCells = 33;

        private static readonly string TemplatePath = @"C:\Users\KOM\Downloads\CardTemplate.png";
        private static readonly string OutputPath = @"C:\Users\KOM\Downloads\BingoCards";

        private static readonly PointF Title = new PointF(3, 7);
        private static Font arial = new Font("Arial", 10);

        public static void PrintPlates(IEnumerable<BingoPlate> plates, string Title = "")
        {
            Bitmap OriginalBitmap = (Bitmap)Image.FromFile(TemplatePath);
            int paddingLength = plates.Count().ToString().Length;
            int totalPlateAmount = plates.Count();
            int counter = 0;

            if (String.IsNullOrEmpty(Title))
            {
                Title = "Bingo dingo";
            }

            foreach (BingoPlate plate in plates)
            {
                counter++;
                Bitmap tempBitmap = new Bitmap(OriginalBitmap);

                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    g.DrawString(Title, arial, Brushes.Black, new PointF(0, 0));
                    g.DrawString($"{counter} of {totalPlateAmount}", new Font("Arial", 8.5f), Brushes.Black, new PointF(0, 12.5f));
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

                            g.DrawString(plate.Card[y, x].ToString(), arial, Brushes.Blue, drawPos);
                        }
                    }
                }

                string savePath = Path.Combine(OutputPath, $"BingoCard_{counter.ToString().PadLeft(paddingLength, '0')}.png");
                tempBitmap.Save(savePath);
            }
        }
    }
}
