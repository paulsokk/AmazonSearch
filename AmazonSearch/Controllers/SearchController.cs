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
    public class ProductsController : Controller
    {

        // GET: Search
        public ActionResult Search(string keyword, string page, string currency)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return View();
            }

            //Makes sure when the user types the url out of bounds, it returns the user to page 1-6
            int pageNum = Int32.Parse(page);
            if (pageNum  < 1)
            {
                pageNum = 1;
            }
            else if(pageNum > 6)
            {
                pageNum = 6;
            }

            //Send to view
            ViewBag.Search = keyword;
            ViewBag.Page = pageNum;
            ViewBag.NextPage = pageNum+1;
            ViewBag.PrevPage = pageNum-1;
            ViewBag.Currency = currency;

            var authentication = new AmazonAuthentication();
            authentication.AccessKey = "ABC";
            authentication.SecretKey = "ABC";

            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK, "ABC");
            var responseGroup = AmazonResponseGroup.ItemAttributes | AmazonResponseGroup.Images | AmazonResponseGroup.Offers | AmazonResponseGroup.OfferFull;
            var searchOperation = wrapper.ItemSearchOperation(keyword, AmazonSearchIndex.All, responseGroup);
            //var result = wrapper.Search(keyword, AmazonSearchIndex.All, responseGroup);
            searchOperation.Skip(pageNum);
            var xml = wrapper.Request(searchOperation);
            var result = XmlHelper.ParseXml<ItemSearchResponse>(xml.Content);

            return View(result);
            //return Content("Hello world?");
        }
    }
}