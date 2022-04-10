using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class UnitTest3
    {
        [Test]
        public void TestPositive()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestNegative()
        {
            Assert.IsTrue(false);
        }
    }
}
