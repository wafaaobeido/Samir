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
    public class ImageSQLContext : IImage
    {
        #region Fields

        private string CS = ConfigurationManager.ConnectionStrings["LOCALDATABASE"].ConnectionString;



        #endregion

        #region methodes

        public Image AddImage(Image image)
        {
            Image newimage = image;
            SqlConnection con = new SqlConnection(CS);
            try
            {
                con.Open();
                string query = "INSERT INTO Image(ImagePath, ProductID)" +
                                          "Values ( @ImagePath, @ProductID)";
                SqlCommand addimage = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                {
                    new SqlParameter("@ImagePath", image.ImagePath),
                    new SqlParameter("@ProductID", image.ProductID)
                }
                };

                int id = Convert.ToInt32(addimage.ExecuteScalar());
                image.Id = id;

                return newimage;
            }
            finally
            {
                con.Close();
            }


        }



        public List<string> GetImageForProduct(int id)
        {
            Product estate = new Product();

            string sql = "SELECT * From Image Where ProductID = @Id";

            var Images = new List<string>();

            using (SqlConnection conn = new SqlConnection(CS))
            {
                conn.Open();
                Image image = new Image();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.Text,
                    CommandText = sql,
                    Parameters = { new SqlParameter("@Id", id) }
                };
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    image.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    image.ImagePath = (string)rdr["ImagePath"];

                    Images.Add(image.ImagePath);
                }

            }
            return Images;
        }

        #endregion
    }
}
