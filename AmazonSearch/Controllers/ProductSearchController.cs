using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazonSearch.Controllers
{
    public class ProductSearchController : Controller
    {

        private AmazonAuthentication GetConfig()
        {
            var accessKey = ConfigurationManager.AppSettings["AKIAJ3HVK3BZHWILSSOA"];
            var secretKey = ConfigurationManager.AppSettings["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"];

            var authentication = new AmazonAuthentication();
            authentication.AccessKey = accessKey;
            authentication.SecretKey = secretKey;

            return authentication;
        }

        // GET: ProductSearch
        public ActionResult ProductSearch()
        {
            string search = "canon";

            ViewBag.Search = search;

            //var authentication = this.GetConfig();
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "***";
            authentication.SecretKey = "***";

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK, "***");
            var responseGroup = AmazonResponseGroup.ItemAttributes | AmazonResponseGroup.Images | AmazonResponseGroup.Offers;
            var result = wrapper.Search(search, AmazonSearchIndex.All, responseGroup);

            return View(result);
            //return Content("Hello world?");
        }

        public ActionResult Test() {
            return Content("This is test!");
        }
    }
}