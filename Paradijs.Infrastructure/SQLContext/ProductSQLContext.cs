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

        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }
        public static string X = ConfigurationManager.ConnectionStrings["LOCALDATABASE"].ConnectionString;

        #endregion

        #region methodes


        public List<Product> ViewProducts()
        {

            String query = "SELECT * FROM Product";
            var model = new List<Product>();
            var ImageContext = new ImageSQLContext();
            using (SqlConnection con = new SqlConnection(X))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    model.Add(Utils.ProductFromReader(reader));
                }
            }
            return model;
        }

        public List<Product> ViewProductDetails(int id)
        {

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
                    model.Add(Utils.ProductFromReader(rdr));
                }
            }
            return model;

        }

        public Product AddProduct(Product product)
        {
            Product newproduct = new Product();

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
            newproduct = product;

            //Merge those two lines//
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
