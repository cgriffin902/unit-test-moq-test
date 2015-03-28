using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razor.Models
{
    public interface IDiscount
    {
        decimal ApplyDiscount(decimal totalParam);
    }
    public class DiscountHelper : IDiscount
    {
        //public decimal DiscountSize { get; set; }
        public decimal discountSize;
        public DiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (discountSize / 100M * totalParam));
        }

    }
}
