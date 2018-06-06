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
    public class ProductSQLContext : IProduct
    {
        #region Fields

        private CS_Databse cs_database = new CS_Databse();

        #endregion

        #region methodes

        public List<Product> All()
        {

            string query = "SELECT * from Product";

            var model = new List<Product>();
            var ImageContext = new ImageSQLContext();
            using (SqlConnection con = new SqlConnection(cs_database.CS()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Product();
                    p.Id = Convert.ToInt32(reader["Id"]);
                    p.Name = (string)reader["Name"];
                    p.Ingredients = (string)reader["Ingredients"];
                    p.Price = Convert.ToDouble(reader["Price"]);
                    p.Picture = ImageContext.GetImageForProduct(Convert.ToInt32(reader["Id"]));
                    model.Add(p);
                }
            }
            return model;
        }

        public Product Details(int id)
        {

            String sql = "SELECT Id, Name, Ingredients FROM Product Where Id = @id";
            var model = new Product();
            using (SqlConnection conn = new SqlConnection(cs_database.CS()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model = Utils.ProductFromReader(rdr);
                }
            }
            return model;

        }

        public Product AddProduct(Product product)
        {
            Product newproduct = new Product();

            SqlConnection con = new SqlConnection(cs_database.CS());
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
            newproduct = product;

            //Merge those two lines//
            int x = Convert.ToInt32(addproduct.ExecuteScalar());
            newproduct.Id = x;

            con.Close();
            return newproduct;

        }

        public Product ByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs_database.CS()))
            {
                string query = "SELECT * FROM Product Where Id = @id";

                Product product = new Product();

                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@id", id)
                    }
                };

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    product = Utils.ProductFromReader(rdr);
                }

                conn.Close();

                return product;
            }
        }

        //Bijwerken ......

        public void DeleteProduct(Product product)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "Delete From Order Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void EditProduct(Product product)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());
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
