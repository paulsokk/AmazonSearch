using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazonSearch.Models;


namespace AmazonSearch.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ProductsView()
        {
            var newProduct1 = new Product(){
                Name = "Keyboard",
                Price = 15.60f,
                ImgURL = "http://icons.iconseeker.com/png/fullsize/longhorn-r2/keyboard-1.png"
            };
            
            //var simpleProduct = new Product() { Name = "Ball" };
            return View(newProduct1);
            //return Content("Hello world?"+ simpleProduct.Name);
        }
    }
}