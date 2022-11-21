using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleBingoPlateGenerator
{
    public class BingoPlate
    {
        static Random rng = new Random();

        public static int[] CreateRow()
        {
            int[] row = new int[9];

            Random rng = new Random();

            var strBinary = Convert.ToString(rng.Next(1, 256 + 1), 2).ToCharArray();

            foreach (var item in strBinary.Select((c, i) => (c, i)))
            {
                row[item.i] = (int)item.c;
            }

            return row;

        }

        public static List<string> CreateRows(int amount)
        {
            List<string> rows = new List<string>();

            while (rows.Count < amount)
            {
                var row1 = ShuffleSingleRow();
                var row2 = ShuffleSingleRow();

                if (TryCreateLastRow(row1, row2, out List<int> row3))
                {
                    rows.Add(CreateId(row1, row2, row3));
                }
            }

            foreach (var item in rows)
            {
                int result = 0;
                foreach (var x in item.ToCharArray())
                {
                    result += x - '0';
                    //result += Convert.ToInt32(x);
                }
                if (result != 35)
                {
                    throw new Exception("NOT RIGHT");
                }
            }
            return rows;
        }

        

        public static List<int> ShuffleSingleRow()
        {
            var row = new List<int>() { 1, 1, 1, 1, 1, 0, 0, 0, 0 };

            row.Shuffle();
            //for (int i = 0; i < row.Count; i++)
            //{
            //    int index = rng.Next(i, row.Count);
            //    var temp = row[i];
            //    row[i] = row[index];
            //    row[index] = temp;
            //}

            return row;
        }

        private static bool TryCreateLastRow(List<int> row1, List<int> row2, out List<int> row3)
        {

            row3 = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> availablePositions = new List<int>();

            for (int i = 0; i < row1.Count; i++)
            {
                if (row1[i] + row2[i] == 2)
                {
                    continue;
                }
                availablePositions.Add(i);
            }

            while (availablePositions.Count > 5)
            {
                availablePositions.RemoveAt(rng.Next(availablePositions.Count));
            }
            if (availablePositions.Count != 5)
            {
                return false;
            }
            foreach (var item in availablePositions)
            {
                row3[item] = 1;
            }

            return true;
        }


        private static string CreateId(params IEnumerable<int>[] ints)
        {
            Console.WriteLine();
            ints.Shuffle();
            //for (int i = 0; i < ints.Length; i++)
            //{
            //    int index = rng.Next(i, ints.Length);
            //    var temp = ints[i];
            //    ints[i] = ints[index];
            //    ints[index] = temp;
            //}

            string id = "";
            var row1 = ints[0].ToArray();
            var row2 = ints[1].ToArray();
            var row3 = ints[2].ToArray();

            for (int i = 0; i < ints[1].Count(); i++)
            {
                id += (row1[i] + (row2[i] * 2) + (row3[i] * 4)).ToString();
            }

            return id;
        }


    }
}
