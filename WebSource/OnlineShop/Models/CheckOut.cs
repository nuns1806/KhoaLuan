using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class CheckOut : Master
    {
        public LinkedList<ObjCheckOut> Products { get; set; }
        public string coupon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }

        public void Insert()
        {
            //Tinh tong gia tri
            int total = 0;
            Product objProduct = new Product();
            foreach(ObjCheckOut item in Products)
            {
                Dictionary<string, string> tempProduct = objProduct.Get(item.ProductId);

                total += int.Parse(tempProduct["GIA_BAN"]) * item.Number;
            }
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                Guid newId = Guid.NewGuid();
                string query = "INSERT INTO DAT_HANG (MA_PHIEU_DAT_HANG,[NGAY_DAT],[NGAY_GIAO],[TONG_TIEN],[TRANG_THAI]) VALUES(@id, @date, @giao, @total, @status)";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@date", SqlDbType.DateTime);
                command.Parameters.Add("@giao", SqlDbType.DateTime);
                command.Parameters.Add("@total", SqlDbType.Int);
                command.Parameters.Add("@status", SqlDbType.NVarChar);

                command.Parameters["@id"].Value = newId;
                command.Parameters["@date"].Value = DateTime.Now;
                command.Parameters["@giao"].Value = DateTime.Now.AddDays(7);
                command.Parameters["@total"].Value = total;
                command.Parameters["@status"].Value = "New";

                command.ExecuteNonQuery();

                foreach(ObjCheckOut item in Products)
                {
                    Dictionary<string, string> tempProduct = objProduct.Get(item.ProductId);
                    int subTotal = int.Parse(tempProduct["GIA_BAN"]) * item.Number;
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO [CHI_TIET_DAT_HANG] ([MA_PHIEU_DAT_HANG],[MA_KHACH_HANG],[MA_NHAN_VIEN],[MA_CHI_NHANH], [MA_SAN_PHAM], [SO_LUONG], [DON_GIA], [THANH_TIEN]) " +
                        "VALUES ('" + newId.ToString() + "','95B0B303-71A5-4106-B0C0-D9D08B67C3BA','D03EA7CB-1036-409E-AC24-364485582C2C','13F6F475-2A47-4B94-93BE-424C93484802','" + item.ProductId + "'," + item.Number + "," + tempProduct["GIA_BAN"] + "," + subTotal + ")";
                    command.ExecuteNonQuery();

                }

                conn.Close();
            }
        }
    }

    public struct ObjCheckOut
    {
        public string ProductId { get; set; }
        public int Number { get; set; }
    }
}