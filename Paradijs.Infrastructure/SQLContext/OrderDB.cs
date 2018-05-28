using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDB
    {
        #region Fields

        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }

        #endregion

        #region methodes

        // Bijwerken

        #region Order
        public void AddOrder()
        {
            Order order = new Order();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "INSERT INTO Order(Id, UserId, OrderTime, DeliveryTime" +
             " VALUES (@Id, @UserId, @OrderTime, @DeliveryTime)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@UserId", order.User.Id);
            cmd.Parameters.AddWithValue("@OrderTime", order.Ordertime);
            cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliveryTime);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteOrder()
        {
            Order order = new Order();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Delete From Order Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditOrder()
        {
            Order order = new Order();
            SqlConnection con = new SqlConnection(connectionstring());
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



        #endregion
    }
}
