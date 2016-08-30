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

            //KEYS
            string amazonAccessKey = "ABC";
            string amazonSecretKey = "ABC";
            string associateTag = "ABC";

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

            //Send info to view
            ViewBag.Search = keyword;
            ViewBag.Page = pageNum;
            ViewBag.NextPage = pageNum+1;
            ViewBag.PrevPage = pageNum-1;
            ViewBag.Currency = currency;

            //Authentication with keys
            var authentication = new AmazonAuthentication();
            authentication.AccessKey = amazonAccessKey;
            authentication.SecretKey = amazonSecretKey;

            //API request
            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK, associateTag);
            var responseGroup = AmazonResponseGroup.ItemAttributes | AmazonResponseGroup.Images | AmazonResponseGroup.Offers | AmazonResponseGroup.OfferFull;
            var searchOperation = wrapper.ItemSearchOperation(keyword, AmazonSearchIndex.All, responseGroup);
            searchOperation.Skip(pageNum);
            var xml = wrapper.Request(searchOperation);
            var result = XmlHelper.ParseXml<ItemSearchResponse>(xml.Content);

            return View(result);
        }
    }
}