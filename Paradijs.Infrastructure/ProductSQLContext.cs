using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductSQLContext : IProduct
    {
        #region Fields

        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }

        #endregion

        #region methodes


        public List<Product> ViewProducts()
        {

            String query = "SELECT Id, Name, Price FROM Product";
            ImageSQLContext imageBD = new ImageSQLContext();

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
                    product.Picture = imageBD.GetImageForProduct(Convert.ToInt32(rdr["Id"]));

                    model.Add(product);
                }



            }
            return model;
        }

        public List<Product> ViewProductDetails(int id)
        {
            ImageSQLContext imageBD = new ImageSQLContext();

            String sql = "SELECT Id, Name, Ingredients FROM Product Where Id = @id";


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
                    product.Id = Convert.ToInt32(rdr["Id"]);
                    product.Name = (string)rdr["Name"];
                    product.Ingredients = (string)rdr["Ingredients"];
                    product.Picture = imageBD.GetImageForProduct(Convert.ToInt32(rdr["Id"]));

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
            string query = "INSERT INTO Product(Name, Ingredients, Price) Values ( @Name, @Ingredients, @Price ); SELECT SCOPE_IDENTITY()";
            SqlCommand addproduct = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters =
                {
                    new SqlParameter( "@Name", product.Name),
                    new SqlParameter("@Ingredients", product.Ingredients),
                    new SqlParameter("@Price", product.Price)
                }
            };

            int x = Convert.ToInt32(addproduct.ExecuteScalar());
            newproduct.Id = x;
            con.Close();
            return newproduct;

        }

        //Bijwerken ......

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
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters =
                {
                    new SqlParameter( "@Id", product.Id),
                    new SqlParameter( "@Name", product.Name),
                    new SqlParameter("@Ingredients", product.Ingredients),
                    new SqlParameter("@Price", product.Price)
                }
            };

            cmd.ExecuteNonQuery();
            con.Close();
        }

        #endregion

    }
}
