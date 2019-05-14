using Moq;
using NUnit.Framework;
using System;

namespace MyUnitTest
{
    [TestFixture]
    public class MyTest
    {
        Mock myInterfaceMock;

     
        [TestCase]
        public void TestAddisTrue()
        {
            var func = new DemoUnitTest.Functions();
            int resultExpected = 11;
            int resultFunc = func.add(5, 6);
            Assert.AreEqual(resultExpected, resultFunc);
        }

        [TestCase]
        public void TestAddisFalse()
        {
            var func = new DemoUnitTest.Functions();
            int resultExpected = 6;
            int resultFunc = func.add(5, 6);
            Assert.AreEqual(resultExpected, resultFunc);
        }

        [TestCase]
        public void TestCheckOdd()
        {
            var func = new DemoUnitTest.Functions();
            bool resultFunc = func.checkOdd(6);
            Assert.IsTrue(resultFunc);
        }

        [TestCase]
        public void DemoMoq()
        {
            int numberToReturn = 4;
            Mock< DemoUnitTest.Functions> moqResult = new Mock<DemoUnitTest.Functions> ();
            moqResult.CallBase = true;
            moqResult.Setup(x => x.SquareOfRandom()).Returns(numberToReturn);

            var results = moqResult.Object.SquareOfRandom();

            Assert.AreEqual(4, results);
        }
    }
}
