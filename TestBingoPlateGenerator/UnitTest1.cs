using ConsoleBingoPlateGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestBingoPlateGenerator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var foo = BingoPlate.CreateRow();

            Assert.IsTrue(true);
        }
    }
}
