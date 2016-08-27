﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazonSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductsView()
        {
            ViewBag.Message = "Here you can search for Amazon products:";

            return View();
        }

        public ActionResult ProductSearch()
        {
            ViewBag.Message = "Here you can search for Amazon products:";

            return View();
        }
    }
}