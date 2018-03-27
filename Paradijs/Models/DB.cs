using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Paradijs.Models
{
    public class DB
    {
        #region Fields
       
        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }

        #endregion

        #region User
        public void AddUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "INSERT INTO User(FirstName, LastName, Birthday, Adress, Postcode, City, Email, Mobile, Password" +
             " VALUES (@FirstName, @LastName, @Birthday, @Adress, @Postcode, @City, @Email, @Mobile,  @Password)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
            cmd.Parameters.AddWithValue("@Adress", user.Adress);
            cmd.Parameters.AddWithValue("@Postcode", user.Postcode);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Delete From User Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void EditUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Update User Set (FirstName = @FirstName, LastName = @LastName, Birthday = @Birthday, Adress =  @Adress, Postcode = @Postcode, City = @City, Email = @Email, Mobile = @Mobile,  Password = @Password) Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LasttName", user.LastName);
            cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
            cmd.Parameters.AddWithValue("@Adress", user.Adress);
            cmd.Parameters.AddWithValue("@Postcode", user.Postcode);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region Product

        //private Product product;
        public List<Product> ViewProducts()
        {

            String query = "SELECT Id, Name, Price FROM Product";
            

            var model = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionstring()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                    var product = new Product();
                        product.Id = Convert.ToInt32(rdr["Id"]);
                        product.Name = (string)rdr["Name"];
                        product.Price = Convert.ToInt32(rdr["Price"]);

                        model.Add(product);
                    }


               
            }
            return model;
        }

        public List<Product> ViewProductDetails(int id)
        {
            
            String sql = "SELECT  Name, Ingredients FROM Product Where Id = @id";


            var model = new List<Product>();
            using (SqlConnection conn = new SqlConnection(connectionstring()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = new Product();
                    
                    product.Name = (string)rdr["Name"];
                    product.Ingredients = (string)rdr["Ingredients"];
                   
                    model.Add(product);
                }

            }

            return model;

        }

        public Product AddProduct( Product product)
        {
            Product newproduct = product;

            SqlConnection con = new SqlConnection(connectionstring());
            con.Open();
            string query = "INSERT INTO Product(Name, Ingredients, Price) Values ( @Name, @Ingredients, @Price )";
            SqlCommand addproduct = new SqlCommand(query, con);
            addproduct.Parameters.AddWithValue("@Name", product.Name);
            addproduct.Parameters.AddWithValue("@Ingredients", product.Ingredients);
            addproduct.Parameters.AddWithValue("@Price", product.Price);
            //addproduct.ExecuteNonQuery();
       

            newproduct.Id = Convert.ToInt32(addproduct.ExecuteScalar());
            con.Close();
            return newproduct;

        }
        public void DeleteProduct(Product product)
        {
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Delete From Order Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditProduct(Product product)
        {
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Update Product Set (Name = @Name, Ingredients = @Ingredients, Price =  @Price )Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Ingredients", product.Ingredients);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

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

        #region Review

        public void AddReview()
        {
            Review review = new Review();
            SqlConnection con = new SqlConnection(connectionstring());
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
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Delete From Review Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", review.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditReview()
        {
            Review review = new Review();
            SqlConnection con = new SqlConnection(connectionstring());
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

        #region Photo's

        #endregion
    }
}