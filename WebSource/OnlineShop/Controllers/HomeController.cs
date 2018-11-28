using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Product objProducts = new Product();
            ViewBag.Products = objProducts.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(string id, int value)
        {
            Dictionary<string, int> sessionData = (Dictionary<string, int>)Session["shoppingcart"];
            if (sessionData == null)
            {
                sessionData = new Dictionary<string, int>();
            }
            if (sessionData.ContainsKey(id))
            {
                sessionData[id] += value;
            } else
            {
                sessionData.Add(id, value);
            }
            Session["shoppingcart"] = sessionData;

            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add( "count", sessionData.Count );
            return Json(result);
        }

        public ActionResult Cart()
        {
            Product objProducts = new Product();
            Dictionary<string, int> sessionData = (Dictionary<string, int>)Session["shoppingcart"];

            List<Dictionary<string, string>> renderValues = new List<Dictionary<string, string>>();
            if (sessionData != null)
            {
                var proIds = sessionData.Keys.ToArray();
                var Products = objProducts.GetAll(proIds);

                foreach (var item in Products)
                {
                    item.Add("SO_LUONG", sessionData[item["MA_SAN_PHAM"]].ToString());
                    renderValues.Add(item);
                }
            }

            ViewBag.Products = renderValues;
            return View();
        }

        [HttpPost]
        public ActionResult RemoveFromCart(string id)
        {
            Dictionary<string, int> sessionData = (Dictionary<string, int>)Session["shoppingcart"];
            sessionData.Remove(id);
            Session["shoppingcart"] = sessionData;

            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("count", sessionData.Count);
            return Json(result);
        }
    }
}