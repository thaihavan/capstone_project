using IdentityProvider.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace IdentityProvider.Test
{
    [TestFixture]
    public class Test
    {
        
        [SetUp]
        public void config()
        {
          
        }


        [TestCase]
        public void test()
        {
            int number = 3;
            Assert.AreEqual(number,3);
        }

        [TestCase]
        public void TestGet()
        {
            var accRe = new AccountRepository();
            Models.Account acc = accRe.Get("asfa");
            Assert.IsNull(acc);

        }
    }
}
