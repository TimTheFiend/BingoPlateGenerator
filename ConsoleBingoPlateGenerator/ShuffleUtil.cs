using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBingoPlateGenerator
{
    public static class ShuffleUtil
    {
        private static Random rng = new Random();


        public static void Shuffle<T>(this IList<T> target)
        {
            for (int i = 0; i < target.Count; i++)
            {
                int index = rng.Next(i, target.Count);
                var temp = target[i];
                target[i] = target[index];
                target[index] = temp;
            }
        }

        public static void RemoveRandom<T>(this IList<T> target) {
            target.RemoveAt(rng.Next(target.Count));
        }

        public static void AddRandomValues(this string target)
        {
            for (int i = 0; i < 9; i++)
            {
                target += rng.Next(0, 10).ToString();
            }
        }
    }
}
