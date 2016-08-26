using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazonSearch.Controllers
{
    public class ProductSearchController : Controller
    {
        // GET: ProductSearch
        public ActionResult ProductSearch()
        {
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "AKIAI6FV3NUY3BEI2FZA";
            authentication.SecretKey = "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";

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK);
            var result = wrapper.Search("ball", AmazonSearchIndex.All, AmazonResponseGroup.Large);

            return View(result);
            //return Content("Hello world?");
        }

        public ActionResult Test() {
            return Content("This is test!");
        }
    }
}