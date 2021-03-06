﻿using System;
using System.Collections.Generic;
using System.Data;
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

        #region methodes

        #region User

        // Registration

        public bool IsEmailExists(User user)
        {
            SqlConnection con = new SqlConnection(connectionstring());

            try
            {
                con.Open();

                string query = "SELECT EmailID FROM [User] Where EmailID = @EmailID";
                SqlCommand checkEmail = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email)
                    }
                };

                int check = Convert.ToInt32(checkEmail.ExecuteScalar());

                return check > 0;
            }
            finally
            {
                con.Close();
            }
        }
        public User AddUser(User user)
        {
            var newuser = new User();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open();
            string query = "INSERT INTO [User](FirstName, LastName, DateOfBirth, Adress, Postcode, City, EmailID, Mobile, Password, ConfirmPassword, IsEmailVerified, ActivationCode)" +
             " VALUES(@FirstName, @LastName, @DateOfBirth, @Adress, @Postcode, @City, @EmailID, @Mobile,  @Password, @ConfirmPassword, @IsEmailVerified, @ActivationCode)";

            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters = {
                    new SqlParameter("@FirstName", user.FirstName),
            new SqlParameter("@LastName", user.LastName),
            new SqlParameter("@DateOfBirth", user.DateOfBirth),
            new SqlParameter("@Adress", user.Adress),
            new SqlParameter("@Postcode", user.Postcode),
            new SqlParameter("@City", user.City),
            new SqlParameter("@EmailID", user.Email),
            new SqlParameter("@Mobile", user.Mobile),
            new SqlParameter("@Password", user.Password),
            new SqlParameter("@ConfirmPassword", user.ConfirmPassword),
            new SqlParameter("@IsEmailVerified", user.IsEmailVerified),
            new SqlParameter("@ActivationCode", user.ActivationCode)
                             }

            };

            cmd.ExecuteNonQuery();
            newuser = user;
            con.Close();
            return newuser;
        }

        public bool IsActivationCodeExists(User user)
        {
            SqlConnection con = new SqlConnection(connectionstring());

            try
            {
                con.Open();

                string query = "SELECT ActivationCode FROM [User] Where ActivationCode = @ActivationCode";
                SqlCommand checkCode = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@ActivationCode", user.ActivationCode)
                    }
                };

                var check = checkCode.ExecuteScalar();

                return check != null;
            }
            finally
            {
                con.Close();
            }


        }

        public void IsValidation(User user)
        {
            SqlConnection con = new SqlConnection(connectionstring());

            try
            {
                con.Open();

                string query = "Update [User] Set IsEmailVerified = @IsEmailVerified " +
                    " where ActivationCode = @ActivationCode";
                SqlCommand isvalidate = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@IsEmailVerified", user.IsEmailVerified),
                        new SqlParameter("@ActivationCode", user.ActivationCode)
                    }
                };

                isvalidate.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        // LogIn
        public string LogIn(User user)
        {
            SqlConnection con = new SqlConnection(connectionstring());
            User newuser = new User();
            try
            {
                con.Open();

                //string query = "SELECT FirstName FROM [User] Where EmailID = @EmailID AND Password = @Password";
                SqlCommand Logincmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usplogin ",
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email),
                        new SqlParameter("@Password", Crypto.Hash(user.Password))
                    }
                };

                var emai = Logincmd.ExecuteScalar().ToString();
                return emai;
               

            }
            finally
            {
                con.Close();
            }

        }


        // Bijwerken users
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
            con.Open(); string query = "Update User Set (FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Adress =  @Adress, Postcode = @Postcode, City = @City, Email = @Email, Mobile = @Mobile,  Password = @Password) Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LasttName", user.LastName);
            cmd.Parameters.AddWithValue("@DatOfBirth", user.DateOfBirth);
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

        public Product AddProduct(Product product)
        {
            Product newproduct = product;

            SqlConnection con = new SqlConnection(connectionstring());
            con.Open();
            string query = "INSERT INTO Product(Name, Ingredients, Price) Values ( @Name, @Ingredients, @Price )";
            SqlCommand addproduct = new SqlCommand(query, con);
            addproduct.Parameters.AddWithValue("@Name", product.Name);
            addproduct.Parameters.AddWithValue("@Ingredients", product.Ingredients);
            addproduct.Parameters.AddWithValue("@Price", product.Price);


            int x = Convert.ToInt32(addproduct.ExecuteScalar());
            newproduct.Id = x;
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

        #region Images

        public Image AddImage(Image image)
        {
            Image newimage = image;

            SqlConnection con = new SqlConnection(connectionstring());
            con.Open();
            string query = "INSERT INTO Image(ImagePath, ImageTitle) Values ( @ImagePath, @ImageTitle )";
            SqlCommand addimage = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters =
                {
                    new SqlParameter("@ImagePath", image.ImagePath),
                    new SqlParameter("@ImageTitle", image.ImageTitle)
                }
            };


            newimage.Id = Convert.ToInt32(addimage.ExecuteScalar());
            con.Close();
            return newimage;


        }

        #endregion

        #endregion
    }
}
