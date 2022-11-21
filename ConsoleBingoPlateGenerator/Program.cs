using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleBingoPlateGenerator
{
    internal class Program
    {
        static Random rng = new Random();

        static void Main(string[] args)
        {
            //BingoPlate.CreateRows(1000000);

            BingoFactory.CreatePlates(10);
            Console.ReadLine();
        }

        static void GenerateBingoPlate()
        {
            const int Rows = 3;
            const int Columns = 9;
            const int EmptyCells = 4;

            const int MinValue = 0;
            const int MaxValue = 9;

            Random rng = new Random();
            string rowContainer = "";

            for (int i = 0; i < Rows; i++)
            {
                var nums = Enumerable.Range(MinValue, MaxValue).ToList();
                int[] emptyCellPos = new int[4];
                for (int j = 0; j < EmptyCells; j++)
                {
                    int pos = rng.Next(nums.Count);
                    emptyCellPos[j] = nums[pos];
                    nums.RemoveAt(pos);
                }
                if (emptyCellPos.Length != 4)
                {
                    throw new Exception("Error with empty spaces.");
                }
                Console.WriteLine(ConvertToPlate(emptyCellPos));
                //emptyCellPos.OrderBy(x => x).ToList();
                emptyCellPos.OrderBy(x => x).ToList().ForEach(Console.Write);
                //Console.WriteLine(emptyCellPos.Select(x => x.ToString()));
                //foreach (var item in emptyCellPos.OrderBy(x => x))
                //{
                //    Console.writeli(item);
                //}
                Console.WriteLine();
            }

        }

        static string ConvertToPlate(IEnumerable<int> row)
        {
            string result = "";
            for (int i = 0; i < 9; i++)
            {
                result += row.Contains(i) ? "_" : "X";
            }
            //if (result.Length != 9)
            //{
            //    throw new Exception("Too many columns");
            //}
            return result;
        }

        static void FisherYates()
        {
            Random rng = new Random();

            List<int> foo = new List<int>()
            {
                1,
                1,
                1,
                1,
                1,
                0,
                0,
                0,
                0
            };
            var pos = new bool[]
            {
                true, true, true, true, true, false, false, false, false
            };

            for (int i = 0; i < pos.Length - 1; i++)
            {
                int index = rng.Next(i, pos.Length);
                var temp = pos[i];
                pos[i] = pos[index];
                pos[index] = temp;
            }
            for (int j = 0; j < 2; j++)
            {

                for (int i = 0; i < foo.Count - 1; i++)
                {
                    int index = rng.Next(i, foo.Count);
                    var temp = foo[i];
                    foo[i] = foo[index];
                    foo[index] = temp;
                }

                foreach (var item in foo)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
        }

        static void FisherYatesProper()
        {

            foreach (var item in ShuffleRow())
            {
                foreach (var _ in item)
                {
                    Console.Write(_);
                }
                Console.WriteLine();
            }
            return;
            var top = ShuffleRow();
            var mid = ShuffleRow();

            foreach (var item in top)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            foreach (var item in mid)
            {
                Console.Write(item);
            }
            Console.WriteLine();


        }

        static List<int[]> ShuffleRow()
        {
            List<int[]> rows = new List<int[]>();

            Random rng = new Random();
            //Random rng = new Random((int)DateTime.UtcNow.Ticks);

            for (int j = 0; j < 2; j++)
            {
                var row = GetRow.ToArray();
                for (int i = 0; i < row.Length - 1; i++)
                {
                    int index = rng.Next(i, row.Length);
                    var temp = row[i];
                    row[i] = row[index];
                    row[index] = temp;
                }
                rows.Add(row);
            }

            return rows;
        }


        static void CreateThirdRow()
        {
            
            #region Top and Mid Row
            var top = GetRow;

            //for (int i = 0; i < top.Count - 1; i++)
            //{
            //    int index = rng.Next(i, top.Count);
            //    var temp = top[i];
            //    top[i] = top[index];
            //    top[index] = temp;
            //}

            var mid = GetRow;

            //for (int i = 0; i < top.Count - 1; i++)
            //{
            //    int index = rng.Next(i, mid.Count);
            //    var temp = mid[i];
            //    mid[i] = mid[index];
            //    mid[index] = temp;
            //}

            for (int i = 0; i < top.Count; i++)
            {
                int tIndex = rng.Next(i, top.Count);
                int mIndex = rng.Next(i, mid.Count);

                var tTemp = top[i];
                var mTemp = mid[i];
                top[i] = top[tIndex];
                mid[i] = mid[mIndex];
                top[tIndex] = tTemp;
                mid[mIndex] = mTemp;


            }
            #endregion

            //string print = "";
            //foreach (var item in mid)
            //{
            //    print += item.ToString();
            //}
            //Console.WriteLine(print);
            //print = "";
            //foreach (var item in top)
            //{
            //    print += item.ToString();
            //}
            //Console.WriteLine(print);


            List<int> availablePosition = new List<int>();

            for (int i = 0; i < top.Count; i++)
            {
                int t = top[i];
                int m = mid[i];
                if (t == m && t == 1)
                {
                    continue;
                }
                availablePosition.Add(i);
            }
            while (availablePosition.Count > 5)
            {
                availablePosition.RemoveAt(rng.Next(availablePosition.Count));
            }
            //for (int i = 0; i < 9; i++)
            //{
            //    Console.Write(availablePosition.Contains(i) ? "1" : "0");
            //}

            CalculateId(top, mid, availablePosition);
        }

        

        static void CalculateId(List<int> top, List<int> mid, List<int> bot)
        {
            string result = "";
            int assert35 = 0;
            for (int i = 0; i < top.Count; i++)
            {
                int t = top[i];
                int m = mid[i];
                int b = bot.Contains(i) ? 1 : 0;
                int value = t + (m * 2) + (b * 4);
                assert35 += value;
                result += value.ToString();
            }
            Console.WriteLine(result);
            if (assert35 != 35)
            {
                Console.WriteLine("^ NOT EQUAL 35");
                //throw new Exception("Doesn't equal 35");
            }
        }

        static List<int> GetRow => new List<int>() { 1, 1, 1, 1, 1, 0, 0, 0, 0 };
    }
}
