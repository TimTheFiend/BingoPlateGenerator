using ConsoleBingoPlateGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestBingoPlateGenerator
{
    [TestClass]
    public class BingoFactoryTest
    {
        const int platesAmount = 10000;
        HashSet<string> plates = BingoFactory.CreatePlates(platesAmount);

        const int posLength = 9;
        const int valueLength = 5;

        [TestMethod]
        public void NoZeroValueInFirstColumn()
        {
            bool noZero = true;

            int topPos = posLength;
            int midPos = topPos + valueLength;
            int botPos = midPos + valueLength;

            foreach (string id in plates)
            {
                string tValue = id.Substring(topPos, 1);
                string mValue = id.Substring(midPos, 1);
                string bValue = id.Substring(botPos, 1);
                switch (id.Substring(0, 1))
                {
                    case "1":
                        noZero = tValue != "0";
                        break;
                    case "2":
                        noZero = mValue != "0";
                        break;
                    case "3":
                        goto case "1";
                    case "4":
                        noZero = bValue != "0";
                        break;
                    case "5":
                        goto case "1";
                    case "6":
                        goto case "2";
                    default:
                        break;
                }

                if (noZero == false) { break; }
            }

            Assert.IsTrue(noZero);
        }
    }
}
