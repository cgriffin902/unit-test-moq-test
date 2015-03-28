using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Razor.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscount discounter;
        private static int counter = 0;
        public LinqValueCalculator(IDiscount discountParam)
        {
            discounter = discountParam;
            System.Diagnostics.Debug.WriteLine(string.Format("Instance{0} created", ++counter));
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}