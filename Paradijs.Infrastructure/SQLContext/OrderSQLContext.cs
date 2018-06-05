using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderSQLContext : IOrder
    {
        #region Fields

        private string CS = ConfigurationManager.ConnectionStrings["LOCALDATABASE"].ConnectionString;

        #endregion

        #region methodes

        public Order GetInfo(int clientid, int productid, int hostid)
        {
            Order o = new Order();

            //......................\\
            string getproduct = "SELECT * FROM Product WHERE Id = @productid";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand(getproduct, con);
            con.Open();
            cmd.Parameters.AddWithValue("@productid", productid);
            cmd.ExecuteNonQuery();
            var model = new List<Product>();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    model.Add(Utils.ProductFromReader(rdr));
                }
                o.product = model[0];
            }


            //......................\\
            string verkoperquery = "SELECT Id, FirstName FROM [User] WHERE Id = @verkoperid";
            SqlCommand cmd3 = new SqlCommand(verkoperquery, con);
            cmd3.Parameters.AddWithValue("@verkoperid", hostid);
            cmd3.ExecuteNonQuery();
            using (SqlDataReader rdr3 = cmd3.ExecuteReader())
            {
                while (rdr3.Read())
                {
                    o.verkoper.Id = Convert.ToInt32(rdr3["Id"]);
                    o.verkoper.FirstName = (string)rdr3["FirstName"];
                }
            }


            //......................\\
            string rentquery = "SELECT Id, FirstName FROM [User] WHERE Id = @userid";
            SqlCommand cmd2 = new SqlCommand(rentquery, con);
            cmd2.Parameters.AddWithValue("@userid", clientid);
            cmd2.ExecuteNonQuery();
            using (SqlDataReader rdr2 = cmd2.ExecuteReader())
            {
                while (rdr2.Read())
                {
                    o.User.Id = Convert.ToInt32(rdr2["Id"]);
                    o.User.FirstName = (string)rdr2["FirstName"];
                }
            }
            con.Close();


            return o;
        }


        public string AddOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            SqlConnection con = new SqlConnection(CS);
            Order o = new Order();

            ////Get the userid qua the firstname...
            //string querynaam = "SELECT Id FROM User WHERE FirstName = @username";
            //SqlCommand cmdnaam = new SqlCommand(querynaam, con);

            o.verkoper.Id = Verkoperid;
            o.User.Id = Klantid;
            o.product.Id = Productid;

            string message = "";
            //Insert into orders the informtions including the userd and productid
            string query = "INSERT INTO [Order] (UserId, OrderTime, DeliveryTime) VALUES (@klantid, @OrderTime, @DeliveryTime)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@klantid", o.User.Id);
            //cmd.Parameters.AddWithValue("@productid", o.product.Id);
            cmd.Parameters.AddWithValue("@OrderTime", o.Ordertime);
            cmd.Parameters.AddWithValue("@DeliveryTime", o.DeliveryTime);
            cmd.ExecuteNonQuery();
            con.Close();
            message = "succes";

            return message;
        }

        public string KoppelTabelOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            SqlConnection con = new SqlConnection(CS);
            Order o = new Order();


            o.verkoper.Id = Verkoperid;
            o.User.Id = Klantid;
            o.product.Id = Productid;

            string message = "";

            //Insert the productid and the orderid to order-product tabel
            string queryKoppel = "INSERT INTO [Order_Product] (OrderId, ProductId, Quantity) " +
                       "VALUES((Select Id From[Order] where Id = (select max(Id) from[Order]  where UserId = @userid)), @productid, @quantity)";
            con.Open();
            SqlCommand cmdKoppel = new SqlCommand(queryKoppel, con);
            cmdKoppel.Parameters.AddWithValue("@userid", o.User.Id);
            cmdKoppel.Parameters.AddWithValue("@productid", o.product.Id);
            cmdKoppel.Parameters.AddWithValue("@quantity", quantity);
            cmdKoppel.ExecuteNonQuery();
            con.Close();

            message = "succes";

            return message;
        }
        public List<Order> ShowOrders(int id)
        {
            SqlConnection con = new SqlConnection(CS);
            string message = "";
            string qry = "Select Name, Price, OrderTime, DeliveryTime " +
                         "from [Order_Product] " +
                         "INNER JOIN [Order] On [Order].Id = [Order_Product].OrderId " +
                         "INNER JOIN [Product] On [Product].Id = [Order_Product].ProductId " +
                         "WHERE UserId = @userid";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@userid", id);
            var model = new List<Order>();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var o = new Order();
                    o.product.Name = (string)rdr["Name"];
                    o.product.Price = Convert.ToDouble(rdr["Price"]);
                    o.Ordertime = Convert.ToDateTime(rdr["Ordertime"]);
                    o.DeliveryTime = Convert.ToDateTime(rdr["DeliveryTime"]);
                    model.Add(o);
                }
            }
            con.Close();
            message = "succes";

            return model;
        }

        // should be deleted
        public void StandardMessage(int UserHostID, int UserRecipientID, string messages, string Subject, int ProductID)
        {
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(
                @"INSERT INTO [Message] (RecipientID, SenderID, ProductID, Subject, Body) VALUES (@userid_recipient, @userid_sender, @productid, @subject, @body)", conn);
            cmd1.Parameters.AddWithValue("@userid_recipient", UserRecipientID);
            cmd1.Parameters.AddWithValue("@userid_sender", UserHostID);
            cmd1.Parameters.AddWithValue("@productid", ProductID);
            cmd1.Parameters.AddWithValue("@subject", Subject);
            cmd1.Parameters.AddWithValue("@body", messages);
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        public List<Order> OrdersByUsers()
        {
            List<Order> ordersbyuser = new List<Order>();
            SqlConnection conn = new SqlConnection(CS);
            string q = "Select [User].FirstName, [User].EmailID , [User].Postcode, [User].Adress, count([Order].Id) as totaalbestelt " +
                       "From[User] " +
                       "Full Outer Join[Order] on[User].Id = [Order].UserId " +
                       "WHERE [User].EmailID != 'samirobeido76@gmail.com' " +
                       "Group by[User].FirstName, [User].EmailID , [User].Postcode, [User].Adress";

            conn.Open();
            SqlCommand cmd = new SqlCommand(q, conn);


            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Order orders = new Order();
                    orders.User.FirstName = rdr.GetString(0);
                    orders.User.Email = rdr.GetString(1);
                    orders.User.Postcode = rdr.GetString(2);
                    orders.User.Adress = rdr.GetString(3);
                    orders.Id = rdr.GetInt32(4);
                    ordersbyuser.Add(orders);
                }
            }
            conn.Close();
            return ordersbyuser;
        }

        // Bijwerken

        public void DeleteOrder()
        {
            Order order = new Order();
            SqlConnection con = new SqlConnection(CS);
            con.Open(); string query = "Delete From Order Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditOrder()
        {
            Order order = new Order();
            SqlConnection con = new SqlConnection(CS);
            con.Open(); string query = "Update Order Set (UserId = @UserId, OrderTime = @OrderTime, DeliveryTime =  @DeliveryTime )Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@UserId", order.User.Id);
            cmd.Parameters.AddWithValue("@OrderTime", order.Ordertime);
            cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliveryTime);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion
    }
}
