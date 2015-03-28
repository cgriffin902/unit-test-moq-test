using Ninject;
using Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private Product[] products = {
            new Product {ProductId = 1, Name = "Kayak", Decripiton = "A one persons boat", Category = "Watersport",Price = 275M},
            new Product {ProductId = 2, Name = "Lifejacket", Decripiton = "saves lives", Category = "Watersport",Price = 48M},
            new Product {ProductId = 3, Name = "Soccer Ball", Decripiton = "you kick it", Category = "Soccer",Price = 25M},
            new Product {ProductId = 4, Name = "Shorts", Decripiton = "cant play without them", Category = "Soccer",Price = 25M},
        };
        private IValueCalculator calc;
        public HomeController(IValueCalculator calcParam1, IValueCalculator calc2)
        {
            calc = calcParam1;
        }
        public ActionResult Index()
        {
            /*//instanceates Ninject
            IKernel ninjectKernel = new StandardKernel();
            //binds Ninject
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //creats an object using ninject
            IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();*/
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}