using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public ActionResult AddNew(CheckOut formdata)
        {
            formdata.Insert();
            Session["shoppingcart"] = null;
            return Redirect("/Home");
        }

    }
}