using Moq;
using NUnit.Framework;
using System;

namespace MyUnitTest
{
    [TestFixture]
    public class MyTest
    {
        DemoUnitTest.Functions func;
        Mock<DemoUnitTest.Functions> moqResult;

       [SetUp]
        public void Config()
        {
            func = new DemoUnitTest.Functions();
            moqResult = new Mock<DemoUnitTest.Functions>();
        }

     
        [TestCase]
        public void TestAddisTrue()
        {
            int resultExpected = 11;
            int resultFunc = func.add(5, 6);
            Assert.AreEqual(resultExpected, resultFunc);
        }

        [TestCase]
        public void TestAddisFalse()
        {
            int resultExpected = 6;
            int resultFunc = func.add(5, 6);
            Assert.AreEqual(resultExpected, resultFunc);
        }

        [TestCase]
        public void TestCheckOdd()
        {
            bool resultFunc = func.checkOdd(6);
            Assert.IsTrue(resultFunc);
        }

        [TestCase]
        public void DemoMoq()
        {
            int numberToReturn = 4;
            moqResult.CallBase = true;
            moqResult.Setup(x => x.SquareOfRandom()).Returns(numberToReturn);

            var results = moqResult.Object.SquareOfRandom();

            Assert.AreEqual(4, results);
        }
    }
}
