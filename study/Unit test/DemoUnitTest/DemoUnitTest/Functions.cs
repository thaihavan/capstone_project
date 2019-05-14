using System;
using System.Collections.Generic;
using System.Text;

namespace DemoUnitTest
{
    public class Functions
    {
        public int add(int a, int b)
        {
            return a + b;
        }
        public bool checkOdd(int a)
        {
            if (a % 2 == 1)
                return true;
            return false;
        }

        public int MyRandom()
        {
            return new Random().Next(0, 10);
        }

        public virtual int SquareOfRandom()
        {
            int result = MyRandom();

            result *= result;

            return result;

        }

    }
}
