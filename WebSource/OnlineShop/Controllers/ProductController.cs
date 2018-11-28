using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            Product objProducts = new Product();
            ViewBag.Products = objProducts.GetAll();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult ShoppingCart()
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
                    item.Add("subtotal", (sessionData[item["MA_SAN_PHAM"]] * int.Parse(item["GIA_BAN"])).ToString());
                    renderValues.Add(item);
                }
            }

            ViewBag.Products = renderValues;
            return View();
        }
    }
}