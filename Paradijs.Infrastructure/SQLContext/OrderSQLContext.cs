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


        public string AddOrder(int Klantid, int Verkoperid, int Productid)
        {
            SqlConnection con = new SqlConnection(CS);
            Order o = new Order();
            string querynaam = "SELECT Id FROM User WHERE FirstName = @username";
            SqlCommand cmdnaam = new SqlCommand(querynaam, con);
            o.verkoper.Id = Verkoperid;
            o.User.Id = Klantid;
            o.product.Id = Productid;

            string message = "";
            string query = "INSERT INTO [Order] (UserId, OrderTime, DeliveryTime, ProductId) VALUES (@klantid, @OrderTime, @DeliveryTime,  @productid)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@klantid", o.User.Id);
            cmd.Parameters.AddWithValue("@productid", o.product.Id);
            cmd.Parameters.AddWithValue("@OrderTime", o.Ordertime);
            cmd.Parameters.AddWithValue("@DeliveryTime", o.DeliveryTime);
            cmd.ExecuteNonQuery();
            con.Close();
            message = "succes";
            //string queryoccupied = "UPDATE product SET Occupied = 1, RESERVED = 0 WHERE EstateID = @estateid";
            //con.Open();
            //SqlCommand cmdocc = new SqlCommand(queryoccupied, con);
            //cmdocc.Parameters.AddWithValue("@estateid", Productid);
            //cmdocc.ExecuteNonQuery();
            //con.Close();
            return message;
        }

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
