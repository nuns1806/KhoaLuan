using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Master
    {
        protected static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
    }
}