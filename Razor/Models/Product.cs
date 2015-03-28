﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Razor.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Decripiton { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}