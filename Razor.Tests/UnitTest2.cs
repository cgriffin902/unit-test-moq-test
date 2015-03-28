using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Razor.Models;
namespace Razor.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = 
        {
            new Product{Name = "Kayak", Category = "Watersports", Decripiton = "One person boat", Price = 275M, ProductId = 1},
             new Product{Name = "LifeJacket", Category = "Watersports", Decripiton = "saves lives", Price = 25M, ProductId = 2},
              new Product{Name = "Soccer ball", Category = "Soccer", Decripiton = "a sphear you kick", Price = 10M, ProductId = 3},
               new Product{Name = "Shorts", Category = "Soccer", Decripiton = "Cant play without them", Price = 50M, ProductId = 4},
        };
        [TestMethod]
        public void Sub_Products_Correctly()
        {
            //arrange
            Mock<IDiscount> mock = new Mock<IDiscount>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            //Act
            var result = target.ValueProducts(products);

            //Assert
            Assert.AreEqual(products.Sum(e => e.Price), result);

        }
        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            //arrange
            Mock<IDiscount> mock = new Mock<IDiscount>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();

            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M)); 

            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v >= 10 && v <= 100))).Returns<decimal>(total => total - 5);

            var target = new LinqValueCalculator(mock.Object);

            //act
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDisocunt = target.ValueProducts(createProduct(10));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            //assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 fail");
            Assert.AreEqual(5, TenDollarDisocunt, "$10 fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 fail");
            target.ValueProducts(createProduct(0));
        }
    }
}