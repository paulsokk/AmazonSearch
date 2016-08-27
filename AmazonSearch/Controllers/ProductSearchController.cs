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

        // GET: ProductSearch
        public ActionResult ProductSearch(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return View();
            }
            


            ViewBag.Search = keyword;

            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "ABC";
            authentication.SecretKey = "ABC";

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK, "ABC");
            var responseGroup = AmazonResponseGroup.ItemAttributes | AmazonResponseGroup.Images | AmazonResponseGroup.Offers | AmazonResponseGroup.OfferFull;
            var searchOperation = wrapper.ItemSearchOperation(keyword, AmazonSearchIndex.All, responseGroup);
            //var result = wrapper.Search(keyword, AmazonSearchIndex.All, responseGroup);
            searchOperation.Skip(1);
            var xml = wrapper.Request(searchOperation);

            var result = XmlHelper.ParseXml<ItemSearchResponse>(xml.Content);

            return View(result);
            //return Content("Hello world?");
        }
    }
}