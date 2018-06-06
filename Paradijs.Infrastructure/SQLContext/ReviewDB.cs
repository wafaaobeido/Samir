using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReviewDB
    {
        #region Fields

        private CS_Databse cs_database = new CS_Databse();

        #endregion

        #region methodes

        // Bijwerken

        #region Review

        public void AddReview()
        {
            Review review = new Review();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "INSERT INTO Review(Id, Buyer, Content, PublishingTime, ProductId" +
             " VALUES (@Id, @Buyer, @Content, @PublishingTime, @ProductId)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", review.Id);
            cmd.Parameters.AddWithValue("@Buyer", review.Buyer);
            cmd.Parameters.AddWithValue("@Content", review.Content);
            cmd.Parameters.AddWithValue("@PublishingTime", review.PublishingTime);
            cmd.Parameters.AddWithValue("@ProductId", review.Product.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteReview()
        {
            Review review = new Review();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "Delete From Review Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", review.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditReview()
        {
            Review review = new Review();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "Update Review Set (Byuer = @Buyer, Content = @Content, PiblishingTime =  @PublishingTime, ProductId= @ProductId )Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", review.Id);
            cmd.Parameters.AddWithValue("@Buyer", review.Buyer);
            cmd.Parameters.AddWithValue("@Content", review.Content);
            cmd.Parameters.AddWithValue("@PublishingTime", review.PublishingTime);
            cmd.Parameters.AddWithValue("@ProductId", review.Product.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        #endregion

        #endregion
    }
}
