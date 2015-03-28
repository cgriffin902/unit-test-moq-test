using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Razor.Models;
namespace Razor.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscount getTestObject()
        {
            return new MinimumDiscountHelper();
        }
        [TestMethod]
        public void Discount_Above_100()
        {
            // arange
            IDiscount target = getTestObject();
            decimal total = 200;
            //act 
            var discountedTotal = target.ApplyDiscount(total);
            //assert
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }
        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            //arrange
            IDiscount target = getTestObject();
            //act
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundreadDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);
            //assert
            Assert.AreEqual(5, TenDollarDiscount, "10$ discount is wrong");

                Assert.AreEqual(95, HundreadDollarDiscount,"100$ discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "50$ is discount wrong");
        }
        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //arrange
            IDiscount target = getTestObject();
            //act
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);
            //assert
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0,discount0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Nagative_Total()
        {
            //arrange
            IDiscount target = getTestObject();
            //act
            target.ApplyDiscount(-1);
        }
    }
}
