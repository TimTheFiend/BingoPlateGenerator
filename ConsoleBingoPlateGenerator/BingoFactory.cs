using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBingoPlateGenerator
{
    public static class BingoFactory
    {
        private static Random rng = new Random();

        public static void CreatePlates(int amount)
        {
            List<string> plates = new List<string>();
            //HashSet<string> plates = new HashSet<string>();
            while (plates.Count < amount)
            {
                List<int> row1 = GetShuffledRow;
                List<int> row2 = GetShuffledRow;
                if (TryCreateRow(row1, row2, out List<int> row3))
                {
                    plates.Add(CreateId(row1, row2, row3));
                }
            }
            CreateRandomValues(ref plates);
            //plates = CreateRandomValues(plates);

#if DEBUG
            foreach (var plate in plates)
            {

                Console.WriteLine(plate);
            }
#endif
        }

        private static List<int> GetShuffledRow
        {
            get
            {
                var row = new List<int>() { 1, 1, 1, 1, 1, 0, 0, 0, 0 };
                row.Shuffle();

                return row;
            }
        }

        private static List<int> GetEmptyRow => new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private static bool TryCreateRow(List<int> row1, List<int> row2, out List<int> row3)
        {
            row3 = GetEmptyRow;
            List<int> availablePositions = new List<int>();

            for (int i = 0; i < row1.Count; i++)
            {
                if (row1[i] + row2[i] == 2)
                    continue;
                availablePositions.Add(i);
            }
            // If there isn't room for row values;
            if (availablePositions.Count < 5)
                return false;

            while (availablePositions.Count > 5)
            {
                availablePositions.RemoveRandom();
            }

            foreach (int pos in availablePositions)
            {
                row3[pos] = 1;
            }
            return true;
        }


        private static string CreateId(params IList<int>[] rows)
        {
            string id = "";
            rows.Shuffle();
            // NOTE `assertValue` is for debugging purposes
            int assertValue = 0;

            for (int i = 0; i < rows[0].Count; i++)
            {
                int value = rows[0][i] + (rows[1][i] * 2) + (rows[2][i] * 4);
                id += value.ToString();
                assertValue += value;
            }
            if (assertValue != 35)
                throw new Exception("NOT 35");

            return id;
        }

        //private static List<string> CreateRandomValues(List<string> bingoPlates)
        private static void CreateRandomValues(ref List<string> bingoPlates)
        {
            const int minValue = 0;
            const int maxValue = 10;

            string top;
            string mid;
            string bot;

            for (int i = 0; i < bingoPlates.Count; i++)
            {
                top = "";
                mid = "";
                bot = "";
                foreach (char item in bingoPlates[i].ToCharArray())
                {
                    int value = item - '0';
                    var values = Enumerable.Range(minValue, maxValue).ToList();
                    int val1 = values[rng.Next(0, values.Count)];
                    values.Remove(val1);
                    int val2 = values[rng.Next(0, values.Count)];

                    switch (value)
                    {
                        case 0:
                            break;
                        case 1:
                            top += rng.Next(minValue, maxValue).ToString();
                            break;
                        case 2:
                            mid += rng.Next(minValue, maxValue).ToString();
                            break;
                        case 4:
                            bot += rng.Next(minValue, maxValue).ToString();
                            break;
                        case 3:
                            top += Math.Min(val1, val2);
                            mid += Math.Max(val1, val2);
                            break;
                        case 5:
                            top += Math.Min(val1, val2);
                            bot += Math.Max(val1, val2);
                            break;
                        case 6:
                            mid += Math.Min(val1, val2);
                            bot += Math.Max(val1, val2);
                            break;
                        default:
                            break;
                    }
                }
                bingoPlates[i] = bingoPlates[i] + top + mid + bot;
            }
            //return bingoPlates;
        }
    }
}
